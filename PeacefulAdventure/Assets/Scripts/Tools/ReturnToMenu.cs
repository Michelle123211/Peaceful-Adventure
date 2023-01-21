using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReturnToMenu : MonoBehaviour
{
    bool alreadyHandled = false;

    void Start()
    {
        PlayerBehaviour.playerInputActions.Player.ReturnToMenu.performed += QuitToMenu;
    }

    private void OnDestroy() {
        PlayerBehaviour.playerInputActions.Player.ReturnToMenu.performed -= QuitToMenu;
    }

    public void QuitToMenu() {
        if (!alreadyHandled) {
            alreadyHandled = true;
            FindObjectOfType<SceneLoader>().LoadSceneWithState("MainMenu");
        }
    }
    private void QuitToMenu(InputAction.CallbackContext context) => QuitToMenu();
}
