using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AppearHideComponent))]
public class InventoryUI : MonoBehaviour {

    [SerializeField] private ItemDetailsUI itemDetailsUI;
    [SerializeField] private InventoryContentUI inventoryContentUI;

    public void Open() {
        PlayerBehaviour.playerInputActions.Player.Disable();
        PlayerBehaviour.playerInputActions.UI.Enable();
        itemDetailsUI.gameObject.SetActive(false);
        inventoryContentUI.SetInventoryUI(this);
        GetComponent<AppearHideComponent>().Do();
        inventoryContentUI.Open();
    }

    public void Close() {
        PlayerBehaviour.playerInputActions.UI.Disable();
        PlayerBehaviour.playerInputActions.Player.Enable();
        GetComponent<AppearHideComponent>().Undo();
    }

    public void ShowDetails(InventoryItem item) {
        itemDetailsUI.Open(item);
    }
}
