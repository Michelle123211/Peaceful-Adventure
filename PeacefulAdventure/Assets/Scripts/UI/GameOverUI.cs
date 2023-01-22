using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameOverUI : MonoBehaviour
{
    private bool initialized = false;

    public void Open() {
        PlayerBehaviour.playerInputActions.Player.Disable();
        PlayerBehaviour.playerInputActions.UI.Enable();
        AudioManager.Instance.PlaySoundEffect(SoundType.UIOpen);
        AudioManager.Instance.PlaySoundEffect(SoundType.GameOver);
        gameObject.TweenAwareEnable();
    }

    public void Close() {
        PlayerBehaviour.playerInputActions.UI.Disable();
        PlayerBehaviour.playerInputActions.Player.Enable();
        AudioManager.Instance.PlaySoundEffect(SoundType.UIClose);
        gameObject.TweenAwareDisable();
    }

    public void RestartGame() {
        if (initialized) {
            // reset the state
            WorldState.Reset(); // saved states for all the scenes
            PlayerState.ResetToInitialState();
            Close();

            Scene currentScene = SceneManager.GetActiveScene();
            // if we are in the tutorial, reload the first scene of the tutorial
            // otherwise, reload the MainMap
            if (currentScene.name.Contains("Tutorial")) {
                FindObjectOfType<SceneLoader>().LoadSceneWithoutState("Tutorial-1-House");
            } else {
                FindObjectOfType<SceneLoader>().LoadSceneWithoutState("MainMap");
            }
        }
    }
    private void RestartGame(InputAction.CallbackContext context) => RestartGame();

    public void QuitToMenu() {
        if (initialized) {
            // reset the state
            WorldState.Reset(); // saved states for all the scenes
            PlayerState.ResetToInitialState();
            Close();
            FindObjectOfType<SceneLoader>().LoadSceneWithoutState("MainMenu");
        }
    }
    private void QuitToMenu(InputAction.CallbackContext context) => QuitToMenu();

    public void SetInitialized() {
        initialized = true;
    }

    private void OnEnable() {
        initialized = false;
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
