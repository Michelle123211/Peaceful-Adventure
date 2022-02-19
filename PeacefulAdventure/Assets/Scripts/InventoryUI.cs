using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AppearHideComponent))]
public class InventoryUI : MonoBehaviour {

    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GridLayoutGroup itemsGrid;
    [SerializeField] private ItemDetailsUI itemDetailsUI;

    private ItemSlotUI[,] slots;

    public void Open() {
        Refresh();
        GetComponent<AppearHideComponent>().Do();
        itemDetailsUI.gameObject.SetActive(false);
    }

    public void Close() {
        GetComponent<AppearHideComponent>().Undo();
    }

    public void Refresh() {
        Debug.Log("InventoryUI refreshed");
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
    }

    public void Clear() {
        // clear slots
        int c = itemsGrid.transform.childCount;
        for (int i = c - 1; i >= 0; --i) {
            GameObject.Destroy(itemsGrid.transform.GetChild(i).gameObject);
        }
    }

    public void ShowDetails(InventoryItem item) {
        itemDetailsUI.Open(item);
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
                slotUI.SetInventoryUI(this);
            }
        }
    }

    private void Start() {
        PlayerState.Instance.inventory.onInventoryChangedCallback += Refresh;
    }
}
