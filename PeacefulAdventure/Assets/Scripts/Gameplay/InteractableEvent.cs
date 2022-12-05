using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractableEvent : Interactable {

    public UnityEvent onInteractionEvent;
    protected override void OnInteraction(InputAction.CallbackContext context) {
        if (onInteractionEvent != null)
            onInteractionEvent.Invoke();
    }
}
