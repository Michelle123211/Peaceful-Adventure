using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class ItemDetailsUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private Button useButton;
    [SerializeField] private GameObject useControl;
    [SerializeField] private InventoryUI inventoryUI;

    [SerializeField] private InventoryContentUI inventoryContent;

    private InventoryItem item;

    public void Open(InventoryItem item) {
        this.item = item;
        // TODO: maybe display item
        // TODO: maybe add image and count
        nameText.text = item.item.itemName;
        descText.text = item.item.description;
        if (!Application.isMobilePlatform)
            useControl.SetActive(item.item.isUsable);
        useButton.interactable = item.item.isUsable;
        inventoryContent.gameObject.TweenAwareDisable();
        gameObject.TweenAwareEnable();
    }

    public void Close() {
        gameObject.TweenAwareDisable();
        inventoryContent.gameObject.TweenAwareEnable();
    }

    public void UseItem() {
        item.item.Use(); // TODO: Show some popup if not possible to use (false returned)
        Close();
    }

    private void OnEnable() {
        PlayerBehaviour.playerInputActions.UI.Action1_J.performed += Use;
        PlayerBehaviour.playerInputActions.UI.Action2_I.performed += CloseInventory;
        PlayerBehaviour.playerInputActions.UI.Action3_L.performed += CloseDetails;
    }

    private void OnDisable() {
        PlayerBehaviour.playerInputActions.UI.Action1_J.performed -= Use;
        PlayerBehaviour.playerInputActions.UI.Action2_I.performed -= CloseInventory;
        PlayerBehaviour.playerInputActions.UI.Action3_L.performed -= CloseDetails;
    }

    private void Use(InputAction.CallbackContext context) {
        UseItem();
    }

    private void CloseInventory(InputAction.CallbackContext context) {
        Close();
        inventoryUI.Close();
    }

    private void CloseDetails(InputAction.CallbackContext context) {
        Close();
    }

}
