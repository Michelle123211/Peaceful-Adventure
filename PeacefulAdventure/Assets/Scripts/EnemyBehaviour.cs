using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    public float maxHealth = 60;
    protected float currentHealth;

    public float attackDamage = 20;
    [Tooltip("Attack every x seconds")]
    public float attackCooldown = 3;
    [Tooltip("How close to the player it can attack")]
    public float attackRange = 1.5f;
    protected float nextAttackTime = 0f;

    public float force = 25f;

    protected Vector2 movement;

    protected PlayerBehaviour player;

    protected Rigidbody2D rb;
    protected CharacterAnimation animator;
    protected SpriteRenderer spriteRenderer;

    protected abstract void ComputeMovement();

    public virtual void TakeDamage(float damage) {
        Debug.Log("Damage taken " + damage);
        currentHealth -= damage;
        if (currentHealth <= 0) {
            Die();
        } else {
            animator.TakeDamage();
            // TODO: play sound effect
        }
    }

    protected virtual void Die() {
        animator.Die();
        // TODO: play sound effect
        spriteRenderer.sortingLayerName = "EnemyBack";
        this.enabled = false;
    }

    protected virtual void Attack() {
        animator.Attack();
    }

    // Start is called before the first frame update
    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<CharacterAnimation>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerBehaviour>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    protected virtual void Update() {
        Vector3 dir = this.player.transform.position - this.transform.position;
        // attack
        if (!this.player.isDead && Time.time > this.nextAttackTime) {
            if (dir.magnitude < attackRange) {
                this.nextAttackTime = Time.time + this.attackCooldown;
                Attack();
            }
        }
        // sorting layers - to appear in front of the player or behind
        if (dir.magnitude < 4) {
            if (dir.y < 0)
                spriteRenderer.sortingLayerName = "EnemyBack";
            else
                spriteRenderer.sortingLayerName = "EnemyFront";
        }
    }

    protected virtual void FixedUpdate() {
        ComputeMovement();
        if (movement != Vector2.zero)
            rb.AddForce(movement * force);
        animator.Move(movement);
    }
}
