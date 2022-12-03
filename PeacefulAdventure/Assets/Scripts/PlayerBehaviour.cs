#define VELOCITY_MOVEMENT
//#define FORCE_MOVEMENT

//#define FORCE_MOVEMENT_ALL_DIR // without restriction to only 4 directions
#define VELOCITY_MOVEMENT_ALL_DIR // without restriction to only 4 directions

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimation))]
public class PlayerBehaviour : MonoBehaviour
{
#if VELOCITY_MOVEMENT
    public float speed = 5f;
#endif
#if FORCE_MOVEMENT
    public float force = 25f;
#endif

    public bool isDead = false;

    [Tooltip("The minimum time between two consecutive damages.")]
    public float damageCooldown = 1f;
    float previousDamage = 0f;

    public static PlayerInputActions playerInputActions;

    Rigidbody2D rb;
    CharacterAnimation animator;

#if FORCE_MOVEMENT
    Vector2 movement;
#endif

    private float nextAttackTime = 0f;

    private InventoryUI inventoryUI;

    public void TakeDamage(float damage) {
        if (Time.time - previousDamage > damageCooldown) { // the current damage has not come too soon after the last one
            Debug.Log("Player damage taken " + damage);
            PlayerState.Instance.UpdateHealth(-damage);
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
    }

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Attack.performed += Attack;
        playerInputActions.Player.Inventory.performed += Inventory;
        playerInputActions.Player.Interaction.performed += Interaction;
#if FORCE_MOVEMENT
        playerInputActions.Player.Movement.performed += Movement;
        playerInputActions.Player.Movement.canceled += Movement;
#endif

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<CharacterAnimation>();
    }

    private void OnDestroy() {
        playerInputActions.Player.Attack.performed -= Attack;
        playerInputActions.Player.Inventory.performed -= Inventory;
        playerInputActions.Player.Interaction.performed -= Interaction;
#if FORCE_MOVEMENT
        playerInputActions.Player.Movement.performed -= Movement;
        playerInputActions.Player.Movement.canceled -= Movement;
#endif
    }

    private void FixedUpdate() {
        if (!this.isDead) {
#if FORCE_MOVEMENT
            rb.AddForce(movement * force);
#endif
#if VELOCITY_MOVEMENT
            Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
            Vector2 movement = Utils.ConvertToFourDirections(inputVector);
#if VELOCITY_MOVEMENT_ALL_DIR
            movement = Utils.ConvertToMovement(inputVector);
#endif
            rb.velocity = movement * speed;
#endif

            animator.Move(movement);
        }
    }

#if FORCE_MOVEMENT
    private void Movement(InputAction.CallbackContext context) {
        movement = Utils.ConvertToFourDirections(context.ReadValue<Vector2>());
#if FORCE_MOVEMENT_ALL_DIR
        movement = Utils.ConvertToMovement(context.ReadValue<Vector2>());
#endif
    }
#endif

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
