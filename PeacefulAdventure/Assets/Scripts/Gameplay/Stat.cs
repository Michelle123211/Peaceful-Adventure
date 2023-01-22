using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {
    [field: SerializeField] public float BaseValue { get; private set; }

    public float Value {
        get {
            if (isCached) return modifiedValue;
            RecomputeValue();
            return modifiedValue;
        }
    }

    private float modifiedValue = 0;
    private bool isCached = false;
    [SerializeField] private List<StatModifier> modifiers = new List<StatModifier>();

    public Stat(float baseValue) {
        this.BaseValue = baseValue;
    }

    public bool IsModified() {
        return Value != BaseValue;
    }

    public void AddModifier(StatModifier modifier) {
        isCached = false;
        modifiers.Add(modifier);
    }

    public void RemoveModifier(StatModifier modifier) {
        isCached = false;
        for (int i = modifiers.Count - 1; i >= 0; i--) {
            if (modifiers[i] == modifier) {
                modifiers.RemoveAt(i);
                return;
            }
        }
    }

    public void ChangeBaseValue(float newValue) {
        isCached = false;
        BaseValue = newValue;
    }

    public void RecomputeValue() {
        modifiers.Sort((a, b) => a.type.CompareTo(b.type));
        float currentValue = BaseValue;
        foreach (StatModifier modifier in modifiers) {
            currentValue = modifier.GetModifiedValue(currentValue);
        }
        modifiedValue = currentValue;
        isCached = true;
    }
}
