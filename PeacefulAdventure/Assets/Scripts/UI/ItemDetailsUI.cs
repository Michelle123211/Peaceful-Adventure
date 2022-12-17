using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AppearHideComponent))]
public class ItemDetailsUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private Button useButton;
    [SerializeField] private InventoryUI inventoryUI;

    private InventoryItem item;

    public void Open(InventoryItem item) {
        this.item = item;
        // TODO: maybe display item
        // TODO: maybe add image and count
        nameText.text = item.item.itemName;
        descText.text = item.item.description;
        useButton.interactable = item.item.isUsable;
        GetComponent<AppearHideComponent>().Do();
    }

    public void Close() {
        GetComponent<AppearHideComponent>().Undo();
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
