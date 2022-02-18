using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    private InventoryItem[] items;
    private int freeSlots;

    public Inventory(int slots) {
        items = new InventoryItem[slots];
        freeSlots = slots;
    }

    public bool AddToInventory(Item item, int count = 1) {
        for (int i = 0; i < items.Length; ++i) {
            if (items[i] == null) continue;
            if (items[i].item == item) {
                items[i].count += count;
                return true;
            }
        }
        if (freeSlots > 0) {
            for (int i = 0; i < items.Length; ++i) {
                if (items[i] == null) {
                    items[i] = new InventoryItem(item, count);
                    --freeSlots;
                    return true;
                }
            }
        }
        return false;
    }

    public bool TakeFromInventory(Item item, int count = 1) {
        for (int i = 0; i < items.Length; ++i) {
            if (items[i] == null) continue;
            if (items[i].item == item && items[i].count >= count) {
                items[i].count -= count;
                if (items[i].count == 0) {
                    items[i] = null;
                    ++freeSlots;
                }
                return true;
            }
        }
        return false;
    }

    public bool HasIninventory(Item item, int count = 1) {
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
