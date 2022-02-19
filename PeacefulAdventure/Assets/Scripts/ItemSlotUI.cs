using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image frame;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI count;
    [SerializeField] private Button detailsButton;

    private Color origColor;

    private InventoryItem item;
    private InventoryUI inventoryUI;

    public void SetItem(InventoryItem item) {
        if (item == null) {
            Clear();
        } else {
            this.item = item;
            this.icon.sprite = item.item.icon;
            this.icon.color = this.icon.color.WithA(1f);
            this.count.gameObject.SetActive(true);
            this.count.text = item.count.ToString();
            this.detailsButton.interactable = true;
        }
    }

    public void SetInventoryUI(InventoryUI inventoryUI) {
        this.inventoryUI = inventoryUI;
    }

    public void Select() {
        this.origColor = this.frame.color;
        this.frame.color = this.selectedColor;
    }

    public void Deselect() {
        if (this.frame.color == this.selectedColor)
            this.frame.color = this.origColor;
    }

    public void ShowDetails() {
        if (this.item != null && this.inventoryUI != null)
            inventoryUI.ShowDetails(this.item);
    }

    private void Clear() {
        this.item = null;
        this.icon.sprite = null;
        this.icon.color = this.icon.color.WithA(0f);
        this.count.gameObject.SetActive(false);
        this.detailsButton.interactable = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
