using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public bool isInnerRange = true;

    public SkeletonBehaviour enemy;

    private float followCountdown = 0;

    private void OnTriggerEnter2D(Collider2D other) {
        if (isInnerRange && other.CompareTag("Player")) {
            enemy.SeesPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!isInnerRange && other.CompareTag("Player")) {
            enemy.SeesPlayer = false;
            enemy.IsFollowing = false;
        }
    }

    private void Awake() {
        followCountdown = enemy.followAfterTime;
    }

    private void Update() {
        if (enemy.SeesPlayer && !enemy.IsFollowing) {
            followCountdown -= Time.deltaTime;
            if (followCountdown < 0) {
                enemy.IsFollowing = true;
            }
        } else {
            followCountdown = enemy.followAfterTime;
        }
    }


}
