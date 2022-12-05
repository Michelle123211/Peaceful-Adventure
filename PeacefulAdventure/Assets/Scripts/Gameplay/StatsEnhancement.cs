using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;


[CreateAssetMenu(fileName = "NewStatsEnhancementItem", menuName = "Inventory/Stat Enhancement Item")]
public class StatsEnhancement : Item {

    [SerializeField] StatModifier attackDamageModifier;
    [SerializeField] StatModifier attackCooldownModifier;

    [Tooltip("Duration of the effect in seconds")]
    public float duration;

    protected override void ApplyEffect() {
        PlayerState playerState = PlayerState.Instance;
        playerState.StartCoroutine(ApplyEffectInternal());
    }

    private IEnumerator ApplyEffectInternal() {
        PlayerState playerState = PlayerState.Instance;
        playerState.attackDamage.AddModifier(attackDamageModifier);
        playerState.attackCooldown.AddModifier(attackCooldownModifier);
        yield return new WaitForSeconds(duration);
        playerState.attackDamage.RemoveModifier(attackDamageModifier);
        playerState.attackCooldown.RemoveModifier(attackCooldownModifier);
    }
}
