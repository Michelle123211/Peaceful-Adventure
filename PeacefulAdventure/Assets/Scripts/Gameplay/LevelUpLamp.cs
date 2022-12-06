using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Experimental.Rendering.Universal;

public class LevelUpLamp : Interactable
{

    public Light2D lampLight;

    protected override void OnInteraction(InputAction.CallbackContext context) {
        // level the player up if they have enough XP
        if (PlayerState.Instance.levelSystem.TryLevelUp()) {
            // TODO: Display the level up UI
        }
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
            lampLight.color = Color.green;
        } else {
            lampLight.color = Color.red;
        }
    }
}