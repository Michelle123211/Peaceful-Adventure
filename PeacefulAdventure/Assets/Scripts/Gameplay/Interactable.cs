using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(ControlHintTrigger))]
public abstract class Interactable : MonoBehaviour
{
    private bool callbackRegistered = false;

    protected abstract void OnInteraction(InputAction.CallbackContext context);

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerBehaviour.playerInputActions.Player.Interaction.performed += OnInteraction;
            callbackRegistered = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerBehaviour.playerInputActions.Player.Interaction.performed -= OnInteraction;
            callbackRegistered = false;
        }
    }
    private void OnDestroy() {
        if (callbackRegistered)
            PlayerBehaviour.playerInputActions.Player.Interaction.performed -= OnInteraction;
    }
}
