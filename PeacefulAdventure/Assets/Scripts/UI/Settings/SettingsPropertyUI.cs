using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsPropertyUI : MonoBehaviour
{
    [Tooltip("If this event is not empty, it will be called no matter in which direction the value was changed.")]
    [SerializeField] UnityEvent onValueChanged;
    [Tooltip("This event is called when the value is decreased.")]
    [SerializeField] UnityEvent onValueDecreased;
    [Tooltip("This event is called when the value is increased.")]
    [SerializeField] UnityEvent onValueIncreased;

    [SerializeField] Image selectedImage;

    public void Select() {
        selectedImage.gameObject.TweenAwareEnable();
    }

    public void Unselect() {
        selectedImage.gameObject.TweenAwareDisable();
    }

    public void ChangeValue(Vector2 direction) {
        if (!Utils.IsNullEvent(onValueChanged)) {
            onValueChanged.Invoke();
        }
        if (direction == Vector2.left && !Utils.IsNullEvent(onValueDecreased)) {
            onValueDecreased.Invoke();
        } else if (direction == Vector2.right && !Utils.IsNullEvent(onValueIncreased)) {
            onValueIncreased.Invoke();
        }
    }
}
