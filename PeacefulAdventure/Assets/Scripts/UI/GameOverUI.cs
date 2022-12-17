using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AppearHideComponent))]
public class GameOverUI : MonoBehaviour
{
    public void Open() {
        PlayerBehaviour.playerInputActions.Player.Disable();
        PlayerBehaviour.playerInputActions.UI.Enable();
        GetComponent<AppearHideComponent>().Do();
    }

    public void Close() {
        PlayerBehaviour.playerInputActions.UI.Disable();
        PlayerBehaviour.playerInputActions.Player.Enable();
        GetComponent<AppearHideComponent>().Undo();
    }

    public void RestartGame() {
        // reset the state
        WorldState.Reset(); // saved states for all the scenes
        PlayerState.ResetToInitialState();
        Close();

        Scene currentScene = SceneManager.GetActiveScene();
        // if we are in the tutorial, reload the first scene of the tutorial
        // otherwise, reload the MainMap
        if (currentScene.name.Contains("Tutorial")) {
            SceneManager.LoadScene("Tutorial-1-House");
        } else {
            SceneManager.LoadScene("MainMap");
        }
    }
    private void RestartGame(InputAction.CallbackContext context) => RestartGame();

    public void QuitToMenu() {
        // reset the state
        WorldState.Reset(); // saved states for all the scenes
        PlayerState.ResetToInitialState();
        Close();
        SceneManager.LoadScene("MainMenu");
    }
    private void QuitToMenu(InputAction.CallbackContext context) => QuitToMenu();

    private void OnEnable() {
        // register input callbacks
        PlayerBehaviour.playerInputActions.UI.Action1_J.performed += RestartGame;
        PlayerBehaviour.playerInputActions.UI.Action3_L.performed += QuitToMenu;
    }

    private void OnDisable() {
        // unregister input callbacks
        PlayerBehaviour.playerInputActions.UI.Action1_J.performed -= RestartGame;
        PlayerBehaviour.playerInputActions.UI.Action3_L.performed -= QuitToMenu;
    }
}
