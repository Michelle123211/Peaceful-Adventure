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
        playerState.AddStatModifier(playerState.attackDamage, attackDamageModifier);
        playerState.AddStatModifier(playerState.attackCooldown, attackCooldownModifier);
        yield return new WaitForSeconds(duration);
        playerState.RemoveStatModifier(playerState.attackDamage, attackDamageModifier);
        playerState.RemoveStatModifier(playerState.attackCooldown, attackCooldownModifier);
    }
}
