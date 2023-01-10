using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    [TextArea]
    public string text;

    [SerializeField] TextMeshProUGUI textField;

    private void Awake() {
        textField.gameObject.SetActive(false);
        textField.text = text;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            textField.gameObject.TweenAwareEnable();// SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            textField.gameObject.TweenAwareDisable();// SetActive(false);
        }
    }
}
