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

    public static PlayerInputActions playerInputActions;
    Rigidbody2D rb;
    CharacterAnimation animator;

#if FORCE_MOVEMENT
    Vector2 movement;
#endif

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

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<CharacterAnimation>();
    }

    private void FixedUpdate() {
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

#if FORCE_MOVEMENT
    private void Movement(InputAction.CallbackContext context) {
        movement = ConvertToFourDirections(context.ReadValue<Vector2>());
    }
#endif

    private void Attack(InputAction.CallbackContext context) {
        Debug.Log("Attack!");
        animator.Attack();
    }

    private void Inventory(InputAction.CallbackContext ocontext) {
        Debug.Log("Inventory!");
    }

    private void Interaction(InputAction.CallbackContext ocontext) {
        Debug.Log("Interaction!");
    }

    private Vector2 ConvertToFourDirections(Vector2 inputVector) {
        float xAbs = Mathf.Abs(inputVector.x);
        float yAbs = Mathf.Abs(inputVector.y);
        if (xAbs < 0.1 && yAbs < 0.1)
            return Vector2.zero;
        if (xAbs > yAbs) {
            if (inputVector.x < 0) return Vector2.left;
            else return Vector2.right;
        } else {
            if (inputVector.y < 0) return Vector2.down;
            else return Vector2.up;
        }
    }
}
