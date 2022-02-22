//#define VELOCITY_MOVEMENT
#define FORCE_MOVEMENT

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimation))]
public class PlayerBehaviour : MonoBehaviour
{
#if VELOCITY_MOVEMENT
    public float speed = 2.5f;
#endif
#if FORCE_MOVEMENT
    public float force = 25f;
#endif

    public bool isDead = false;

    public static PlayerInputActions playerInputActions;
    Rigidbody2D rb;
    CharacterAnimation animator;

#if FORCE_MOVEMENT
    Vector2 movement;
#endif

    private float nextAttackTime = 0f;

    private InventoryUI inventoryUI;

    public void TakeDamage(float damage) {
        Debug.Log("Player damage taken " + damage);
        PlayerState.Instance.UpdateHealth(-damage);
        // TODO: play sound effect
        if (PlayerState.Instance.CurrentHealth <= 0) {
            Die();
        } else {
            animator.TakeDamage();
            // TODO: play sound effect
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

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<CharacterAnimation>();
    }

    private void FixedUpdate() {
        if (!this.isDead) {
#if FORCE_MOVEMENT
            rb.AddForce(movement * force);
#endif
#if VELOCITY_MOVEMENT
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        Vector2 movement = ConvertToFourDirections(inputVector);
        rb.velocity = movement * speed;
#endif

            animator.Move(movement);
        }
    }

#if FORCE_MOVEMENT
    private void Movement(InputAction.CallbackContext context) {
        movement = Utils.ConvertToFourDirections(context.ReadValue<Vector2>());
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
