using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimation))]
public class PlayerBehaviour : MonoBehaviour {
    public float speed = 5f;

    public bool isDead = false;

    [Tooltip("The minimum time between two consecutive damages.")]
    public float damageCooldown = 1f;
    float previousDamage = 0f;

    public static PlayerInputActions playerInputActions;

    [SerializeField] ParticleSystem dustParticles;
    [SerializeField] ParticleSystem bloodParticles;

    [SerializeField] ParticleSystem attackDamageParticles;
    [SerializeField] ParticleSystem attackCooldownParticles;

    Rigidbody2D rb;
    CharacterAnimation animator;

    private float nextAttackTime = 0f;

    private InventoryUI inventoryUI;

    public void TakeDamage(float damage) {
        if (Time.time - previousDamage > damageCooldown) { // the current damage has not come too soon after the last one
            PlayerState.Instance.UpdateHealth((int)-damage);
            bloodParticles.Play();
            if (PlayerState.Instance.CurrentHealth <= 0) {
                Die();
            } else {
                animator.TakeDamage();
                AudioManager.Instance.PlaySoundEffect(SoundType.Damage);
            }
            previousDamage = Time.time;
        } else {
            // it is too soon to take a damage again
        }
    }

    public void Die() {
        animator.Die();
        this.isDead = true;
        // show the UI (comes with a sound effect)
        Utils.FindObject<GameOverUI>()[0].Open();
    }

    public void DoStep() {
        AudioManager.Instance.PlaySoundEffect(SoundType.Step);
    }

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Attack.performed += Attack;
        playerInputActions.Player.Inventory.performed += Inventory;
        playerInputActions.Player.Interaction.performed += Interaction;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<CharacterAnimation>();
    }

    private void Start() {
        PlayerState.Instance.onStatsChangedCallback += RefreshEffectParticles;
        RefreshEffectParticles();
    }

    private void OnDestroy() {
        playerInputActions.Player.Attack.performed -= Attack;
        playerInputActions.Player.Inventory.performed -= Inventory;
        playerInputActions.Player.Interaction.performed -= Interaction;
        PlayerState.Instance.onStatsChangedCallback -= RefreshEffectParticles;
    }

    private void FixedUpdate() {
        if (!this.isDead) {
            Vector2 movement = playerInputActions.Player.Movement.ReadValue<Vector2>();
            if (Mathf.Abs(movement.x) < 0.1 && Mathf.Abs(movement.y) < 0.1)
                movement = Vector2.zero;
            rb.velocity = movement * speed;

            animator.Move(movement);

            // dust particles
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y)) { // left or right
                float x = (movement.x > 0 ? -1 : 1) * 0.3f;
                dustParticles.transform.localPosition = dustParticles.transform.localPosition.WithX(x);
            } else { // up or down
                dustParticles.transform.localPosition = dustParticles.transform.localPosition.WithX(0);
            }
            if (movement != Vector2.zero && dustParticles.isStopped) {
                dustParticles.Play();
            } else if (movement == Vector2.zero && dustParticles.isPlaying) {
                dustParticles.Stop();
            }
        }
    }

    private void Attack(InputAction.CallbackContext context) {
        if (!this.isDead && Time.time > this.nextAttackTime) {
            this.nextAttackTime = Time.time + PlayerState.Instance.attackCooldown.Value;
            animator.Attack();
            AudioManager.Instance.PlaySoundEffect(SoundType.Attack);
        }
    }

    private void Inventory(InputAction.CallbackContext context) {
        if (this.inventoryUI == null) {
            this.inventoryUI = Utils.FindObject<InventoryUI>()[0];
        }
        if (this.inventoryUI != null) {
            this.inventoryUI.Open();
        } else {
            Debug.LogWarning("Missing InventoryUI in the scene.");
        }
    }

    private void Interaction(InputAction.CallbackContext ocontext) {
    }

    private void RefreshEffectParticles() {
        RefreshEffect(PlayerState.Instance.attackDamage, attackDamageParticles);
        RefreshEffect(PlayerState.Instance.attackCooldown, attackCooldownParticles);

    }

    private void RefreshEffect(Stat stat, ParticleSystem particles) {
        if (stat.IsModified() && particles.isStopped) {
            particles.Play();
        } else if (!stat.IsModified() && particles.isPlaying) {
            particles.Stop();
        }
    }
}
