using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewHealthPotion", menuName = "Inventory/Health Potion")]
public class HealthPotion : Item {
    [Tooltip("How much health is restored after using the potion.")]
    [SerializeField] int value;

    protected override void ApplyEffect() {
        PlayerState.Instance.UpdateHealth(value);
    }
}
