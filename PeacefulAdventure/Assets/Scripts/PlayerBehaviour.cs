using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    PlayerInputActions playerInputActions;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Attack.performed += Attack;
        playerInputActions.Player.Inventory.performed += Inventory;
        playerInputActions.Player.Interaction.performed += Interaction;
    }

    private void FixedUpdate() {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        this.gameObject.transform.localPosition += new Vector3(inputVector.x, inputVector.y, 0f) * 0.1f;
    }

    public void Attack(InputAction.CallbackContext context) {
        Debug.Log("Attack!");
    }

    private void Inventory(InputAction.CallbackContext ocontext) {
        Debug.Log("Inventory!");
    }

    private void Interaction(InputAction.CallbackContext ocontext) {
        Debug.Log("Interaction!");
    }
}
