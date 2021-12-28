using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 2;

    PlayerInputActions playerInputActions;
    Rigidbody2D rb;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Attack.performed += Attack;
        playerInputActions.Player.Inventory.performed += Inventory;
        playerInputActions.Player.Interaction.performed += Interaction;
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        Vector2 velocity = ConvertToFourDirections(inputVector);
        rb.velocity = velocity * speed;
    }

    private void Attack(InputAction.CallbackContext context) {
        Debug.Log("Attack!");
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
