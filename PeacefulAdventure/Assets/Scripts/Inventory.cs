using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallback;

    private InventoryItem[] items;
    private int freeSlots;

    public Inventory(int slots) {
        items = new InventoryItem[slots];
        freeSlots = slots;
    }

    public InventoryItem[] GetItems() {
        return this.items;
    }

    public bool AddToInventory(Item item, int count = 1) {
        bool itemAdded = false;
        for (int i = 0; i < items.Length; ++i) {
            if (items[i] == null) continue;
            if (items[i].item == item) {
                items[i].count += count;
                itemAdded = true;
                break;
            }
        }
        if (freeSlots > 0) {
            for (int i = 0; i < items.Length; ++i) {
                if (items[i] == null) {
                    items[i] = new InventoryItem(item, count);
                    --freeSlots;
                    itemAdded = true;
                    break;
                }
            }
        }
        if (itemAdded && onInventoryChangedCallback != null)
            onInventoryChangedCallback.Invoke();
        return itemAdded;
    }

    public bool TakeFromInventory(Item item, int count = 1) {
        bool itemTaken = false;
        for (int i = 0; i < items.Length; ++i) {
            if (items[i] == null) continue;
            if (items[i].item == item && items[i].count >= count) {
                items[i].count -= count;
                if (items[i].count == 0) {
                    items[i] = null;
                    ++freeSlots;
                }
                itemTaken = true;
                break;
            }
        }
        if (itemTaken && onInventoryChangedCallback != null)
            onInventoryChangedCallback.Invoke();
        return itemTaken;
    }

    public bool HasInInventory(Item item, int count = 1) {
        for (int i = 0; i < items.Length; ++i) {
            if (items[i] == null) continue;
            if (items[i].item == item && items[i].count >= count) {
                return true;
            }
        }
        return false;
    }

}

public class InventoryItem {
    public Item item;
    public int count;

    public InventoryItem(Item item, int count = 1) {
        this.item = item;
        this.count = count;
    }
}
