using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tweenable : MonoBehaviour
{
    public UnityEvent onEnableTween;
    public UnityEvent onDisableTween;

    public void Disable() {
        if (Utils.IsNullEvent(onDisableTween)) gameObject.SetActive(false);
        else onDisableTween.Invoke();
    }

    public void Enable() {
        if (Utils.IsNullEvent(onEnableTween)) gameObject.SetActive(true);
        else onEnableTween.Invoke();
    }
}
