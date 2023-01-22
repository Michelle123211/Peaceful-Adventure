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

    public int GetDistinctItemsCount() {
        return items.Length - freeSlots;
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
        if (!itemAdded && freeSlots > 0) {
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

    public bool AddToInventory(InventoryItem item) {
        return AddToInventory(item.item, item.count);
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

    public bool TakeFromInventory(InventoryItem item) {
        return TakeFromInventory(item.item, item.count);
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

    public bool HasInInventory(InventoryItem item) {
        return HasInInventory(item.item, item.count);
    }

    public Inventory Copy() {
        Inventory result = new Inventory(items.Length);
        foreach (InventoryItem item in items) {
            if (item != null && item.item != null) {
                result.AddToInventory(item.item, item.count);
            }
        }
        return result;
    }

}

[System.Serializable]
public class InventoryItem {
    public Item item;
    public int count;

    public InventoryItem(Item item, int count = 1) {
        this.item = item;
        this.count = count;
    }

    public override bool Equals(object obj) {
        if (obj == null) return false;
        InventoryItem other = obj as InventoryItem;
        if (other == null) return false;
        else return Equals(other);
    }
    public override int GetHashCode() {
        return item.GetHashCode() * 13 + count;
    }
    public bool Equals(InventoryItem other) {
        if (other == null) return false;
        return (this.item.Equals(other.item) && this.count == other.count);
    }
}
