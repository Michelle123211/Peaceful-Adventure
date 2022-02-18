using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {
    public float Value {
        get {
            if (isCached) return modifiedValue;
            RecomputeValue();
            return modifiedValue;
        }
    }

    [SerializeField] private float baseValue;
    private float modifiedValue = 0;
    private bool isCached = false;
    private List<StatModifier> modifiers = new List<StatModifier>();

    public Stat(float baseValue) {
        this.baseValue = baseValue;
    }

    public void AddModifier(StatModifier modifier) {
        isCached = false;
        modifiers.Add(modifier);
    }

    public void RemoveModifier(StatModifier modifier) {
        for (int i = modifiers.Count - 1; i >= 0; i--) {
            if (modifiers[i] == modifier) {
                modifiers.RemoveAt(i);
            }
        }
    }

    public void RecomputeValue() {
        modifiers.Sort((a, b) => a.type.CompareTo(b.type));
        float currentValue = baseValue;
        foreach (StatModifier modifier in modifiers) {
            currentValue = modifier.GetModifiedValue(currentValue);
        }
        modifiedValue = currentValue;
        isCached = true;
    }
}
