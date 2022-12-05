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
            animator.SetBool("IsWalking", false);
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
