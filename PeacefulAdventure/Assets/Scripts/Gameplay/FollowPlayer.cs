using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public bool isInnerRange = true;

    public SkeletonBehaviour enemy;

    private void OnTriggerEnter2D(Collider2D other) {
        if (isInnerRange && other.CompareTag("Player")) {
            enemy.IsFollowing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!isInnerRange && other.CompareTag("Player")) {
            enemy.IsFollowing = false;
        }
    }


}
