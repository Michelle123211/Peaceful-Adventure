using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestBehaviour : MonoBehaviour
{
    Animator animator;
    bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OpenOrClose(InputAction.CallbackContext context) {
        this.isOpen = !this.isOpen;
        animator.SetBool("IsOpen", this.isOpen);
        if (this.isOpen) {
            // TODO: show content
        } else { 
            // TODO
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            PlayerBehaviour.playerInputActions.Player.Interaction.performed += OpenOrClose;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            PlayerBehaviour.playerInputActions.Player.Interaction.performed -= OpenOrClose;
        }
    }
}
