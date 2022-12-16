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

    public void SetCurrent(int value) {
        currentValue = value;
        RefreshValues();
    }

    public void SetMaximum(int maximum) {
        maximumValue = maximum;
        RefreshValues();
    }

    public void SetLabel(string label) {
        labelTMP.text = label;
    }

    private void RefreshValues() {
        fillImage.fillAmount = (float) Mathf.Min(currentValue, maximumValue) / maximumValue;
        valueTMP.text = $"{currentValue}/{maximumValue}";
    }
}
