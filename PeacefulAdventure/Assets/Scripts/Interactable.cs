using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    protected abstract void OnInteraction(InputAction.CallbackContext context);

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerBehaviour.playerInputActions.Player.Interaction.performed += OnInteraction;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerBehaviour.playerInputActions.Player.Interaction.performed -= OnInteraction;
        }
    }
}
