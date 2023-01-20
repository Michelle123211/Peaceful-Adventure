using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public UnityEvent onButtonDown;
    public UnityEvent onButtonUp;

    public void OnPointerDown(PointerEventData eventData) {
        AudioManager.Instance.PlaySoundEffect(SoundType.UIPress, 0.3f);
        if (!Utils.IsNullEvent(onButtonDown)) {
            onButtonDown.Invoke();
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        AudioManager.Instance.PlaySoundEffect(SoundType.UIRelease, 0.8f);
        if (!Utils.IsNullEvent(onButtonUp)) {
            onButtonUp.Invoke();
        }
    }
}
