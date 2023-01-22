using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ControlHintTrigger : MonoBehaviour {
    [Header("Controls")]
    [Tooltip("An image of the button used on Android.")]
    public Sprite buttonImage;
    [Tooltip("A name of the key used on Windows.")]
    public string key = "L";
    [Header("Action description")]
    [Tooltip("A description of the action which is invoked by the specified control.")]
    [TextArea]
    public string description = "to interact";

    private ControlHintUI hintUI;

    private void Awake() {
        // Create an instance of the ControlHintUI prefab and store it into a data field
        GameObject prefab = Resources.Load("Prefabs/ControlHintUI") as GameObject;
        hintUI = Instantiate(prefab, transform).GetComponent<ControlHintUI>();
        // Hide it by default
        hintUI.gameObject.SetActive(false);
        // Set its properties
        if (buttonImage == null) {
            buttonImage = Resources.Load("Sprites/interaction") as Sprite;
        }
        hintUI.SetContent(buttonImage, key, description);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Show the ControlHintUI
        if (collision.CompareTag("Player")) {
            hintUI.gameObject.TweenAwareEnable();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        // Hide the ControlHintUI
        if (collision.CompareTag("Player")) {
            hintUI.gameObject.TweenAwareDisable();
        }
    }
}
