using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DoorBehaviour : Interactable
{
    [Tooltip("Scene loaded after an interaction with the door.")]
    [SerializeField] string nextScene;

    public void SetNextScene(string nextScene) {
        this.nextScene = nextScene;
    }

    protected override void OnInteraction(InputAction.CallbackContext context) {
        FindObjectOfType<SceneLoader>().LoadSceneWithState(this.nextScene);
    }
}
