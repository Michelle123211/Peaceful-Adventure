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

    public InventoryItem Item { get;  private set; }
    private InventoryUI inventoryUI;
    private ChestUI chestUI;

    public void SetItem(InventoryItem item) {
        if (item == null || item.item == null) {
            Clear();
        } else {
            this.Item = item;
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

    public void SetChestUI(ChestUI chestUI) {
        this.chestUI = chestUI;
    }

    public void Select() {
        if (Application.isMobilePlatform) return;
        this.origColor = this.frame.color;
        this.frame.color = this.selectedColor;
    }

    public void Deselect() {
        if (Application.isMobilePlatform) return;
        if (this.frame.color == this.selectedColor)
            this.frame.color = this.origColor;
    }

    public void ShowDetails() {
        if (this.Item != null && this.inventoryUI != null) {
            AudioManager.Instance.PlaySoundEffect(SoundType.UIPress);
            inventoryUI.ShowDetails(this.Item);
        }
    }

    public void TakeFromChest() {
        if (this.Item != null && this.chestUI != null)
            chestUI.TakeItem(this.Item);
    }

    private void Clear() {
        this.Item = null;
        this.icon.sprite = null;
        this.icon.color = this.icon.color.WithA(0f);
        this.count.gameObject.SetActive(false);
        this.detailsButton.interactable = false;
    }
}
