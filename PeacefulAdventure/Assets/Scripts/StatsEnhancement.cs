using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewStatsEnhancementItem", menuName = "Inventory/Stat Enhancement Item")]
public class StatsEnhancement : Item {

    [SerializeField] StatModifier attackDamageModifier;
    [SerializeField] StatModifier attackCooldownModifier;

    [Tooltip("Duration of the effect in seconds")]
    public float duration;

    public override void Use() {
        throw new System.NotImplementedException();
    }
}
