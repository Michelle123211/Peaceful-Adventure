using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StatModifier {
    public StatModifierType type;
    public float value;

    public StatModifier(StatModifierType type, float value) {
        this.type = type;
        this.value = 1f;
    }

    public float GetModifiedValue(float baseValue) {
        switch (type) {
            case StatModifierType.Multiplicative:
                return baseValue * value;
            case StatModifierType.Additive:
                return baseValue + value;
            case StatModifierType.AdditivePercent:
                return baseValue + baseValue * value;
            default:
                return baseValue;
        }
    }

    public static bool operator==(StatModifier x, StatModifier y) {
        return x.type == y.type && x.value == y.value;
    }

    public static bool operator !=(StatModifier x, StatModifier y) {
        return !(x == y);
    }

    public override bool Equals(object obj) {
        if (obj == null || !this.GetType().Equals(obj.GetType())) {
            return false;
        }
        StatModifier other = (StatModifier)obj;
        return this == other;
    }

    public override int GetHashCode() {
        return value.GetHashCode() * 17 + type.GetHashCode();
    }

}

public enum StatModifierType {
    Additive,
    Multiplicative,
    AdditivePercent
}
