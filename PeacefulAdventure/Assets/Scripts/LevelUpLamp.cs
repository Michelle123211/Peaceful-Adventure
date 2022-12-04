using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class LevelUpLamp : Interactable
{

    SpriteRenderer spriteRenderer;

    protected override void OnInteraction(InputAction.CallbackContext context) {
        // level the player up if they have enough XP
        if (PlayerState.Instance.levelSystem.TryLevelUp()) {
            // TODO: Display the level up UI
        }
    }
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        UpdateState();
    }

    private void OnEnable() {
        PlayerState.Instance.levelSystem.onExperiencePointsChangedCallback += UpdateState;
        PlayerState.Instance.levelSystem.onLevelChangedCallback += UpdateState;
    }

    private void OnDisable() {
        if (PlayerState.Instance.levelSystem != null) { // might have been destroyed already
            PlayerState.Instance.levelSystem.onExperiencePointsChangedCallback -= UpdateState;
            PlayerState.Instance.levelSystem.onLevelChangedCallback -= UpdateState;
        }
    }

    private void UpdateState() {
        if (PlayerState.Instance.levelSystem.CanLevelUp()) {
            spriteRenderer.color = Color.green;
        } else {
            spriteRenderer.color = Color.red;
        }
    }
}
