using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimation))]
public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 5f;

    public bool isDead = false;

    [Tooltip("The minimum time between two consecutive damages.")]
    public float damageCooldown = 1f;
    float previousDamage = 0f;

    public static PlayerInputActions playerInputActions;

    [SerializeField] ParticleSystem dustParticles;
    [SerializeField] ParticleSystem bloodParticles;

    Rigidbody2D rb;
    CharacterAnimation animator;

    private float nextAttackTime = 0f;

    private InventoryUI inventoryUI;

    public void TakeDamage(float damage) {
        if (Time.time - previousDamage > damageCooldown) { // the current damage has not come too soon after the last one
            Debug.Log("Player damage taken " + damage);
            PlayerState.Instance.UpdateHealth((int) -damage);
            bloodParticles.Play();
            // TODO: play sound effect
            if (PlayerState.Instance.CurrentHealth <= 0) {
                Die();
            } else {
                animator.TakeDamage();
                // TODO: play sound effect
            }
            previousDamage = Time.time;
        } else {
            Debug.Log("Player did not take damage (it is too early).");
        }
    }

    public void Die() {
        Debug.Log("Player died.");
        animator.Die();
        // TODO: play sound effect
        this.isDead = true;
        // show the UI
        Utils.FindObject<GameOverUI>()[0].Open();
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

    private void OnDestroy() {
        playerInputActions.Player.Attack.performed -= Attack;
        playerInputActions.Player.Inventory.performed -= Inventory;
        playerInputActions.Player.Interaction.performed -= Interaction;
    }

    private void FixedUpdate() {
        if (!this.isDead) {
            Vector2 movement = playerInputActions.Player.Movement.ReadValue<Vector2>();
            if (Mathf.Abs(movement.x) < 0.1 && Mathf.Abs(movement.y) < 0.1)
                movement = Vector2.zero;
            rb.velocity = movement * speed;

            animator.Move(movement);

            // dust particles
            if (movement == Vector2.zero && dustParticles.isPlaying) {
                dustParticles.Stop();
            } else if (movement != Vector2.zero && dustParticles.isStopped) {
                dustParticles.Play();
                if (movement.x != 0) {
                    float x = -Mathf.Sign(movement.x) * 0.3f;
                    dustParticles.transform.localPosition = dustParticles.transform.localPosition.WithX(x);
                } else if (movement.y != 0) {
                    dustParticles.transform.localPosition = dustParticles.transform.localPosition.WithX(0);
                }
            }
        }
    }

    private void Attack(InputAction.CallbackContext context) {
        if (!this.isDead && Time.time > this.nextAttackTime) {
            this.nextAttackTime = Time.time + PlayerState.Instance.attackCooldown.Value;
            animator.Attack();
        }
    }

    private void Inventory(InputAction.CallbackContext context) {
        Debug.Log("Inventory!");
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
        Debug.Log("Interaction!");
    }
}
