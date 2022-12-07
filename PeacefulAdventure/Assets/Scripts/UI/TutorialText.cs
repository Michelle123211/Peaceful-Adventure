using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    public string text;

    [SerializeField] TextMeshProUGUI textField;

    private void Awake() {
        textField.gameObject.SetActive(false);
        textField.text = text;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        textField.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        textField.gameObject.SetActive(false);
    }
}
