using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestBehaviour : Interactable
{
    Animator animator;
    bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected override void OnInteraction(InputAction.CallbackContext context) {
        this.isOpen = !this.isOpen;
        animator.SetBool("IsOpen", this.isOpen);
        if (this.isOpen) {
            // TODO: show content
        } else {
            // TODO
        }
    }
}
