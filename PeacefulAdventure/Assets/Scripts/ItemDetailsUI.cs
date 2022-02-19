using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AppearHideComponent))]
public class ItemDetailsUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private Button useButton;

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

}
