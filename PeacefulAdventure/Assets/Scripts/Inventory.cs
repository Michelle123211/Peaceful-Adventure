using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private Item[] items;

    public Inventory(int slots) {
        items = new Item[slots];
    }

    public bool AddToInventory() {
        return false;
    }

    public bool TakeFromInventory() {
        return false;
    }

    public bool HasIninventory() {
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
