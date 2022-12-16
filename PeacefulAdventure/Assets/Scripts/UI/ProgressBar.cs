using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI valueTMP;
    [SerializeField] Image fillImage;
    [SerializeField] TextMeshProUGUI labelTMP;

    int currentValue = 0;
    int maximumValue = 0;

    RectTransform rectTransform;

    public void RefreshValues(int current, int maximum, string label = null) {
        currentValue = current;
        maximumValue = maximum;
        // TODO: Tweening
        fillImage.fillAmount = (float)Mathf.Min(currentValue, maximumValue) / maximumValue;
        valueTMP.text = $"{currentValue}/{maximumValue}";
        if (label != null) {
            labelTMP.text = label;
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }
}
