using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class ItemDetailsUI : MonoBehaviour {
    [Header("UI elements")]
    [Tooltip("A TMP for the name of the item.")]
    [SerializeField] private TextMeshProUGUI nameText;
    [Tooltip("A TMP for the description of the item.")]
    [SerializeField] private TextMeshProUGUI descText;
    [Tooltip("A Button for using the item.")]
    [SerializeField] private Button useButton;
    [Tooltip("An object containing controls description for using the item.")]
    [SerializeField] private GameObject useControl;

    [Header("Related pop-ups")]
    [Tooltip("An object managing the whole inventory UI (parent).")]
    [SerializeField] private InventoryUI inventoryUI;
    [Tooltip("A pop-up displaying content of the inventory (sibling).")]
    [SerializeField] private InventoryContentUI inventoryContent;

    private InventoryItem item;
    private bool initialized = false;

    public void Open(InventoryItem item) {
        this.item = item;
        nameText.text = item.item.itemName;
        descText.text = item.item.description;
        if (!Application.isMobilePlatform)
            useControl.SetActive(item.item.isUsable);
        useButton.interactable = item.item.isUsable;
        AudioManager.Instance.PlaySoundEffect(SoundType.UIOpen);
        gameObject.TweenAwareEnable();
    }

    public void Close() {
        AudioManager.Instance.PlaySoundEffect(SoundType.UIClose);
        gameObject.TweenAwareDisable();
    }

    public void UseItem() {
        if (item.item.Use()) {
            AudioManager.Instance.PlaySoundEffect(SoundType.UIPress);
            inventoryUI.ShowInventoryContent();
        }
    }

    public void SetInitialized() {
        initialized = true;
    }

    private void OnEnable() {
        initialized = false;
        PlayerBehaviour.playerInputActions.UI.Action1_J.performed += Use;
        PlayerBehaviour.playerInputActions.UI.Action3_L.performed += CloseDetails;
    }

    private void OnDisable() {
        PlayerBehaviour.playerInputActions.UI.Action1_J.performed -= Use;
        PlayerBehaviour.playerInputActions.UI.Action3_L.performed -= CloseDetails;
    }

    private void Use(InputAction.CallbackContext context) {
        if (initialized) {
            UseItem();
        }
    }

    private void CloseDetails(InputAction.CallbackContext context) {
        inventoryUI.ShowInventoryContent();
    }

}
