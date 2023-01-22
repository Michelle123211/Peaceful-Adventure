using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [Tooltip("An EnemyBehaviour this component is attached to.")]
    public EnemyBehaviour enemyBehaviour;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerBehaviour>().TakeDamage(enemyBehaviour.attackDamage);
        }
    }

}
