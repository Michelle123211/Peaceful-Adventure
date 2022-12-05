using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DoorBehaviour : Interactable
{
    [SerializeField] string nextScene;

    public void SetNextScene(string nextScene) {
        this.nextScene = nextScene;
    }

    protected override void OnInteraction(InputAction.CallbackContext context) {
        FindObjectOfType<SceneLoader>().LoadScene(this.nextScene);
        //SceneManager.LoadScene(this.nextScene);
    }
}
