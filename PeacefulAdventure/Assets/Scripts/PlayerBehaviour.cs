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
        //playerInputActions.Player.Movement.performed += Movement_performed;
    }

    private void FixedUpdate() {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        this.gameObject.transform.localPosition += new Vector3(inputVector.x, inputVector.y, 0f) * 0.1f;
    }

    private void Movement_performed(InputAction.CallbackContext context) {
        Debug.Log(context);
        Vector2 inputVector = context.ReadValue<Vector2>();
        this.gameObject.transform.localPosition += new Vector3(inputVector.x, inputVector.y, 0f);
    }

    public void Attack(InputAction.CallbackContext context) {
        if (context.performed)
            Debug.Log("Attack! " + context.phase);
    }
}
