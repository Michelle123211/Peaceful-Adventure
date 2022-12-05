using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewHealthPotion", menuName = "Inventory/Health Potion")]
public class HealthPotion : Item {

    [SerializeField] float value;

    protected override void ApplyEffect() {
        PlayerState.Instance.UpdateHealth(value);
    }
}
