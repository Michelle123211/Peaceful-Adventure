using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewCoin", menuName = "Inventory/Coin")]
public class Coin : Item
{
    [SerializeField] private float value;

    protected override void ApplyEffect() {
        throw new System.NotImplementedException();
    }
}
