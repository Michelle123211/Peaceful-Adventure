using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour {

    [SerializeField] private ItemDetailsUI itemDetailsUI;
    [SerializeField] private InventoryContentUI inventoryContentUI;

    public void Open() {
        Debug.Log("Opening inventory");
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

    public void Log(string message) {
        Debug.Log(message);
    }
}
