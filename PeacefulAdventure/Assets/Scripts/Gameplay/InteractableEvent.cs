using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractableEvent : Interactable {

    [Tooltip("An event invoked on interaction with the object this component is assigned to.")]
    public UnityEvent onInteractionEvent;
    protected override void OnInteraction(InputAction.CallbackContext context) {
        if (onInteractionEvent != null)
            onInteractionEvent.Invoke();
    }
}
