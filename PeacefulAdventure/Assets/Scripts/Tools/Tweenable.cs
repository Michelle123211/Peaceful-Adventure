using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tweenable : MonoBehaviour
{
    public UnityEvent onEnableTween;
    public UnityEvent onDisableTween;

    public void Disable() {
        if (IsNull(onDisableTween)) gameObject.SetActive(false);
        else onDisableTween.Invoke();
    }

    public void Enable() {
        if (IsNull(onEnableTween)) gameObject.SetActive(true);
        else onEnableTween.Invoke();
    }

    private bool IsNull(UnityEvent unityEvent) {
        if (unityEvent == null) return true;
        for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++) {
            if (unityEvent.GetPersistentTarget(i) != null) {
                return false;
            }
        }
        return true;
    }
}
