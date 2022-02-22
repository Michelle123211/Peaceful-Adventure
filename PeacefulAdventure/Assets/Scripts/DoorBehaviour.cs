using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DoorBehaviour : Interactable
{
    protected override void OnInteraction(InputAction.CallbackContext context) {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MainMap") {// change scene from MainMap to HouseIndoor
            SceneManager.LoadScene("HouseIndoor");
            Debug.Log("To HouseIndoor");
        } else if (sceneName == "HouseIndoor") {// change scene from HouseIndoor to MainMap
            SceneManager.LoadScene("MainMap");
        }
    }
}
