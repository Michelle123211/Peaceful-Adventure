using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public abstract class Item : ScriptableObject
{
    public string itemName;
    [TextArea(15, 20)]
    public string description;
    public Sprite icon;
    public bool isUsable;

    public bool Use() {
        if (this.isUsable && PlayerState.Instance.inventory.TakeFromInventory(this)) {
            ApplyEffect();
            return true;
        }
        return false;
    }

    protected abstract void ApplyEffect();
}
