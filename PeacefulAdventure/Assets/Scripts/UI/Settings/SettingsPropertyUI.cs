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
        Debug.Log($"Property {gameObject.name} was selected.");
    }

    public void Unselect() {
        selectedImage.gameObject.TweenAwareDisable();
        Debug.Log($"Property {gameObject.name} was UNselected.");
    }

    public void ChangeValue(Vector2 direction) {
        if (!Utils.IsNullEvent(onValueChanged)) {
            Debug.Log($"Value of property {gameObject.name} was changed.");
            onValueChanged.Invoke();
        }
        if (direction == Vector2.left && !Utils.IsNullEvent(onValueDecreased)) {
            Debug.Log($"Value of property {gameObject.name} was decreased.");
            onValueDecreased.Invoke();
        } else if (direction == Vector2.right && !Utils.IsNullEvent(onValueIncreased)) {
            Debug.Log($"Value of property {gameObject.name} was increased.");
            onValueIncreased.Invoke();
        }
    }
}
