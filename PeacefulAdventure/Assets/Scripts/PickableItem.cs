using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickableItem : Interactable
{
    public Item item;

    private SpriteRenderer spriteRenderer;

    protected override void OnInteraction(InputAction.CallbackContext context) {
        if (PlayerState.Instance.inventory.AddToInventory(item)) {
            // item added to the inventory
            Debug.Log("Item " + item.itemName + " was added to the inventory.");
            Destroy(gameObject); // TODO: some effects
        }
    }

    private void Start() {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        if (this.spriteRenderer != null)
            this.spriteRenderer.sprite = item.icon;
    }
}
