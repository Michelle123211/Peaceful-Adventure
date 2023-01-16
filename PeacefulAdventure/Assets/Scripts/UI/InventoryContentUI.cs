using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryContentUI : MonoBehaviour
{
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GridLayoutGroup itemsGrid;

    private ItemSlotUI[,] slots;

    private int selectedRow = 0;
    private int selectedColumn = 0;

    private InventoryUI inventoryUI;

    private bool initialized = false;

    public void Open() {
        Refresh();
        AudioManager.Instance.PlaySoundEffect(SoundType.UIOpen);
        gameObject.TweenAwareEnable();
    }

    public void Close() {
        AudioManager.Instance.PlaySoundEffect(SoundType.UIClose);
        gameObject.TweenAwareDisable();
    }

    public void Refresh() {
        // ensure there is enough slots
        if (this.slots == null)
            AllocateSlots();
        int rows = this.slots.GetLength(0);
        int columns = this.slots.GetLength(1);
        if (rows * columns < PlayerState.Instance.InventorySlots) {
            AllocateSlots();
        }
        // display items
        InventoryItem[] items = PlayerState.Instance.inventory.GetItems();
        for (int i = 0; i < items.Length; i++) {
            this.slots[i / columns, i % columns].SetItem(items[i]);
        }
        // select first slot
        this.slots[selectedRow, selectedColumn].Deselect();
        selectedRow = 0;
        selectedColumn = 0;
        this.slots[selectedRow, selectedColumn].Select();
    }

    public void Clear() {
        // clear slots
        int c = itemsGrid.transform.childCount;
        for (int i = c - 1; i >= 0; --i) {
            GameObject.Destroy(itemsGrid.transform.GetChild(i).gameObject);
        }
    }

    private void AllocateSlots() {
        Clear();
        int numOfSlots = PlayerState.Instance.InventorySlots;
        int columns = itemsGrid.constraintCount;
        int rows = numOfSlots / columns + (numOfSlots % columns == 0 ? 0 : 1);
        this.slots = new ItemSlotUI[rows, columns];
        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < columns; ++j) {
                GameObject newSlot = Instantiate(itemSlotPrefab) as GameObject;
                newSlot.SetActive(true);
                newSlot.transform.SetParent(itemsGrid.transform, false);
                ItemSlotUI slotUI = newSlot.GetComponent<ItemSlotUI>();
                this.slots[i, j] = slotUI;
                slotUI.SetInventoryUI(inventoryUI);
            }
        }
    }

    public void SetInventoryUI(InventoryUI inventoryUI) {
        this.inventoryUI = inventoryUI;
    }

    public void SetInitialized() {
        initialized = true;
    }

    private void OnEnable() {
        initialized = false;
        PlayerState.Instance.inventory.onInventoryChangedCallback += Refresh;
        PlayerBehaviour.playerInputActions.UI.Navigation.performed += Navigate;
        PlayerBehaviour.playerInputActions.UI.Action1_J.performed += Select;
        PlayerBehaviour.playerInputActions.UI.Action2_I.performed += CloseInventory;
        PlayerBehaviour.playerInputActions.UI.Action3_L.performed += CloseInventory;
        Refresh();
    }

    private void OnDisable() {
        if (PlayerState.Instance.inventory != null) // might have been destroyed already
            PlayerState.Instance.inventory.onInventoryChangedCallback -= Refresh;
        PlayerBehaviour.playerInputActions.UI.Navigation.performed -= Navigate;
        PlayerBehaviour.playerInputActions.UI.Action1_J.performed -= Select;
        PlayerBehaviour.playerInputActions.UI.Action2_I.performed -= CloseInventory;
        PlayerBehaviour.playerInputActions.UI.Action3_L.performed -= CloseInventory;
    }

    private void Navigate(InputAction.CallbackContext context) {
        AudioManager.Instance.PlaySoundEffect(SoundType.UIPress);
        this.slots[selectedRow, selectedColumn].Deselect();
        Vector2 delta = Utils.ConvertToFourDirections(context.ReadValue<Vector2>());
        selectedRow -= (int)delta.y;
        selectedColumn += (int)delta.x;
        if (selectedRow < 0)
            selectedRow = this.slots.GetLength(0) - 1;
        if (selectedRow >= this.slots.GetLength(0))
            selectedRow = 0;
        if (selectedColumn < 0)
            selectedColumn = this.slots.GetLength(1) - 1;
        if (selectedColumn >= this.slots.GetLength(1))
            selectedColumn = 0;
        this.slots[selectedRow, selectedColumn].Select();
    }

    private void Select(InputAction.CallbackContext context) {
        if (initialized) {
            AudioManager.Instance.PlaySoundEffect(SoundType.UIPress);
            this.slots[selectedRow, selectedColumn].ShowDetails();
        }
    }

    private void CloseInventory(InputAction.CallbackContext context) {
        inventoryUI.Close();
    }
}
