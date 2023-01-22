using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour {

    [Tooltip("A pop-up displaying details of an item (child).")]
    [SerializeField] private ItemDetailsUI itemDetailsUI;
    [Tooltip("A pop-up displaying content of the inventory (child).")]
    [SerializeField] private InventoryContentUI inventoryContentUI;

    public void Open() {
        PlayerBehaviour.playerInputActions.Player.Disable();
        PlayerBehaviour.playerInputActions.UI.Enable();
        itemDetailsUI.gameObject.SetActive(false);
        inventoryContentUI.SetInventoryUI(this);
        gameObject.SetActive(true);
        //gameObject.TweenAwareEnable();
        inventoryContentUI.Open();
    }

    public void Close() {
        PlayerBehaviour.playerInputActions.UI.Disable();
        PlayerBehaviour.playerInputActions.Player.Enable();
        inventoryContentUI.Close();
    }

    public void ShowDetails(InventoryItem item) {
        itemDetailsUI.Open(item);
    }

    public void ShowInventoryContent() {
        itemDetailsUI.Close();
    }
}
