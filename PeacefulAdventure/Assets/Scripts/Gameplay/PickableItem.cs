using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Experimental.Rendering.Universal;

public class PickableItem : Interactable, ISaveable<bool> {

    public Item item;

    [SerializeField] private Light2D backgroundLight;
    private SpriteRenderer spriteRenderer;

    protected override void OnInteraction(InputAction.CallbackContext context) {
        if (PlayerState.Instance.inventory.AddToInventory(item)) {
            // item added to the inventory
            Debug.Log("Item " + item.itemName + " was added to the inventory.");
            Destroy(gameObject); // TODO: some effects
        }
    }

    private void Awake() {
        SetItemSprite();
        SetLightColor();
    }

    public PositionID GetID() {
        return new PositionID { x = (int)transform.position.x, y = (int)transform.position.y };
    }

    public bool SaveState() {
        return true;
    }

    public void LoadState(bool model) {
        if (!model)
            Destroy(gameObject); // TODO: some effects
    }

    [ContextMenu("Set item sprite")]
    public void SetItemSprite() {
        if (this.spriteRenderer == null)
            this.spriteRenderer = GetComponent<SpriteRenderer>();
        if (this.spriteRenderer != null)
            this.spriteRenderer.sprite = item.icon;
    }

    [ContextMenu("Set light color")]
    public void SetLightColor() {
        if (this.backgroundLight != null)
            this.backgroundLight.color = item.lightColor;
    }
}
