using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;

using DG.Tweening;
using DG.Tweening.Core;

public abstract class EnemyBehaviour : MonoBehaviour {

    [Header("Basic stats")]
    public float maxHealth = 60;
    protected float currentHealth;

    public float attackDamage = 20;
    [Tooltip("Attack every x seconds")]
    public float attackCooldown = 3;
    [Tooltip("How close to the player it can attack")]
    public float attackRange = 1.5f;
    protected float nextAttackTime = 0f;

    [Header("Movement")]
    public float force = 25f;
    protected Vector2 movement;

    [Header("State")]
    public bool isDead = false;

    [Header("Effects")]
    public Light2D enemyLight;
    public ParticleSystem bloodParticles;
    public TextMeshProUGUI healthText;


    protected PlayerBehaviour player;

    protected Rigidbody2D rb;
    protected CharacterAnimation animator;
    protected SpriteRenderer spriteRenderer;

    private float displayedHealth;
    private TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> healthTween;

    protected abstract void ComputeMovement();

    public virtual void TakeDamage(float damage) {
        if (!this.isDead) {
            currentHealth -= damage;
            if (healthTween.IsActive() && !healthTween.IsComplete()) {
                healthTween.Complete();
            }
            healthTween = DOTween.To(() => displayedHealth,
                x => {
                    displayedHealth = x;
                    healthText.text = $"{(int)displayedHealth}/{maxHealth}";
                },
                Mathf.Max(0, currentHealth),
                0.5f);
            healthText.gameObject.GetComponent<GenericTween>()?.DoTween();
            bloodParticles.Play();
            if (currentHealth <= 0) {
                Die();
            } else {
                animator.TakeDamage();
                AudioManager.Instance.PlaySoundEffect(SoundType.Damage);
            }
        }
    }

    protected virtual void Die() {
        animator.Die();
        if (enemyLight != null) {
            enemyLight.enabled = false; // turn off the light
        }
        healthText.gameObject.SetActive(false); // hide the health
        PlayerState.Instance.levelSystem.UpdateExperience(3); // add experience points
        AudioManager.Instance.PlaySoundEffect(SoundType.EnemyDeath);
        spriteRenderer.sortingLayerName = "EnemyBack";
        this.isDead = true;
    }

    protected virtual void Attack() {
        animator.Attack();
        AudioManager.Instance.PlaySoundEffect(SoundType.Attack);
    }

    protected virtual void Awake() {
        currentHealth = maxHealth;
        if (healthText != null) {
            displayedHealth = Mathf.Max(0, currentHealth);
            healthText.text = $"{(int)displayedHealth}/{maxHealth}";
        }
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<CharacterAnimation>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerBehaviour>();
    }


    // Update is called once per frame
    protected virtual void Update() {
        Vector3 dir = this.player.transform.position - this.transform.position;
        // attack
        if (!this.isDead && !this.player.isDead && Time.time > this.nextAttackTime) {
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
        if (!isDead) {
            ComputeMovement();
            if (movement != Vector2.zero)
                rb.AddForce(movement * force);
            animator.Move(movement);
        }
    }
}
