using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AppearHideComponent))]
public class ChestUI : MonoBehaviour
{
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GridLayoutGroup itemsGrid;

    private ChestBehaviour chest;

    private ItemSlotUI[] slots;

    private int selectedIndex = 0;

    public void Open(ChestBehaviour chest) {
        PlayerBehaviour.playerInputActions.Player.Disable();
        PlayerBehaviour.playerInputActions.Chest.Enable();
        this.chest = chest;
        Debug.Log("Opening a chest");
        Refresh();
        GetComponent<AppearHideComponent>().Do();
    }

    public void Close() {
        PlayerBehaviour.playerInputActions.Chest.Disable();
        PlayerBehaviour.playerInputActions.Player.Enable();
        GetComponent<AppearHideComponent>().Undo();
    }

    public void CloseChest() {
        this.chest.Close();
    }

    public void Refresh() {
        Clear();
        // add slots for the items
        for (int i = 0; i < this.chest.items.Count; ++i) {
            GameObject newSlot = Instantiate(itemSlotPrefab);
            newSlot.SetActive(true);
            newSlot.transform.SetParent(itemsGrid.transform, false);
            ItemSlotUI slotUI = newSlot.GetComponent<ItemSlotUI>();
            this.slots[i] = slotUI;
            this.slots[i].SetItem(chest.items[i]);
            this.slots[i].SetChestUI(this);
        }
        // select first slot
        if (selectedIndex >= this.slots.Length) {
            selectedIndex = 0;
        }
        if (selectedIndex < this.slots.Length)
            this.slots[selectedIndex].Select();
    }

    public void Clear() {
        int c = itemsGrid.transform.childCount;
        for (int i = c - 1; i >= 1; --i) { // 0th item is template
            GameObject.Destroy(itemsGrid.transform.GetChild(i).gameObject);
        }
        // make sure at least one empty slot is displayed
        if (this.chest.items == null)
            this.chest.items = new List<InventoryItem>();
        if (this.chest.items.Count == 0)
            this.chest.items.Add(null);
        this.slots = new ItemSlotUI[chest.items.Count];
    }

    public void TakeItem(InventoryItem item) {
        // remove from chest and add to inventory
        if (PlayerState.Instance.inventory.AddToInventory(item)) {
            chest.RemoveItem(item);
        }
        Refresh();
    }

    private void OnEnable() {
        PlayerState.Instance.inventory.onInventoryChangedCallback += Refresh;
        PlayerBehaviour.playerInputActions.Chest.Navigation.performed += Navigate;
        PlayerBehaviour.playerInputActions.Chest.Select.performed += Select;
    }

    private void OnDisable() {
        if (PlayerState.Instance.inventory != null) // might have been destroyed already
            PlayerState.Instance.inventory.onInventoryChangedCallback -= Refresh;
        PlayerBehaviour.playerInputActions.Chest.Navigation.performed -= Navigate;
        PlayerBehaviour.playerInputActions.Chest.Select.performed -= Select;
    }

    private void Navigate(InputAction.CallbackContext context) {
        if (selectedIndex < this.slots.Length)
            this.slots[selectedIndex].Deselect();
        Vector2 delta = Utils.ConvertToFourDirections(context.ReadValue<Vector2>());
        selectedIndex += (int)delta.x;
        if (selectedIndex < 0)
            selectedIndex = Mathf.Max(this.slots.Length - 1, 0);
        if (selectedIndex >= this.slots.Length)
            selectedIndex = 0;
        if (selectedIndex < this.slots.Length)
            this.slots[selectedIndex].Select();
    }

    private void Select(InputAction.CallbackContext context) {
        this.slots[selectedIndex].TakeFromChest();
    }
}