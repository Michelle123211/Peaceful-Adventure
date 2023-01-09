using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public UnityEvent onButtonDown;
    public UnityEvent onButtonUp;

    public void OnPointerDown(PointerEventData eventData) {
        if (!Utils.IsNullEvent(onButtonDown)) {
            onButtonDown.Invoke();
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (!Utils.IsNullEvent(onButtonUp)) {
            onButtonUp.Invoke();
        }
    }
}
