using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearHideComponent : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToAppear;
    [SerializeField] private List<GameObject> objectsToHide;

    public void Do() {
        gameObject.TweenAwareEnable();
        foreach (var go in objectsToAppear)
            go.TweenAwareEnable();
        foreach (var go in objectsToHide)
            go.TweenAwareDisable();
    }

    public void Undo() {
        gameObject.TweenAwareDisable();
        foreach (var go in objectsToAppear)
            go.TweenAwareDisable();
        foreach (var go in objectsToHide)
            go.TweenAwareEnable();
    }
}
