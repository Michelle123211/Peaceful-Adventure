using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlHintUI : MonoBehaviour
{
    public Image buttonImage;
    public TextMeshProUGUI keyText;
    public TextMeshProUGUI actionText;

    public void SetContent(Sprite buttonSprite, string keyName, string actionDescription) {
        this.buttonImage.sprite = buttonSprite;
        this.keyText.text = keyName;
        this.actionText.text = actionDescription;
    }
}
