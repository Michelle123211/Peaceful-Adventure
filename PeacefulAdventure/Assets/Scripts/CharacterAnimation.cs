//#define VELOCITY_MOVEMENT
#define FORCE_MOVEMENT

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterAnimation : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Move(Vector2 movement) {
        if (movement == Vector2.zero) {
#if VELOCITY_MOVEMENT
            animator.SetBool("IsWalking", false);
#endif
#if FORCE_MOVEMENT
            if (rb != null) { // sometimes this method is called from FixedUpdate even before an initialization
                if (rb.velocity.x < 0.1 && rb.velocity.y < 0.1) { // switch animation to idle when the character is actually stopped
                    animator.SetBool("IsWalking", false);
                }
            }
#endif
        } else {
            animator.SetBool("IsWalking", true);
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
    }

    public void Attack() {
        animator.SetTrigger("Attack");
    }

    public void TakeDamage() {
        animator.SetTrigger("Damage");
    }

    public void Die() {
        animator.SetTrigger("Death");
    }
}
