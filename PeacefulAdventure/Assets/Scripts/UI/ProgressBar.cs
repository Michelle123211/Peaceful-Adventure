using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DG.Tweening;
using DG.Tweening.Core;

public class ProgressBar : MonoBehaviour {
    [Tooltip("Value label.")]
    [SerializeField] TextMeshProUGUI valueTMP;
    [Tooltip("Fill image used in the progress bar.")]
    [SerializeField] Image fillImage;
    [Tooltip("Property label.")]
    [SerializeField] TextMeshProUGUI labelTMP;

    int currentValue = 0;
    int maximumValue = 0;

    RectTransform rectTransform;

    public void RefreshValues(int current, int maximum, string label = null) {
        currentValue = current;
        maximumValue = maximum;
        fillImage.DOComplete();
        fillImage.DOFillAmount((float)Mathf.Min(currentValue, maximumValue) / maximumValue, 0.4f);
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
