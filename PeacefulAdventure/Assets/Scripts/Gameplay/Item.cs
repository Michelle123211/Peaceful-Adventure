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
    [Tooltip("The color used for the background light if the item is placed freely on the map.")]
    public Color lightColor;

    public bool Use() {
        if (this.isUsable && PlayerState.Instance.inventory.TakeFromInventory(this)) {
            ApplyEffect();
            return true;
        }
        return false;
    }

    protected abstract void ApplyEffect();
}
