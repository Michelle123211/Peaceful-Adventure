using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreditsUI : MonoBehaviour
{
    private bool initialized = false;

    public void Open() {
        PlayerBehaviour.playerInputActions.Player.Disable();
        PlayerBehaviour.playerInputActions.UI.Enable();
        AudioManager.Instance.PlaySoundEffect(SoundType.UIOpen);
        gameObject.TweenAwareEnable();
    }

    public void Close() {
        PlayerBehaviour.playerInputActions.UI.Disable();
        PlayerBehaviour.playerInputActions.Player.Enable();
        AudioManager.Instance.PlaySoundEffect(SoundType.UIClose);
        gameObject.TweenAwareDisable();
    }

    public void SetInitialized() {
        initialized = true;
    }

    private void OnEnable() {
        initialized = false;
        // register input callbacks
        PlayerBehaviour.playerInputActions.UI.Action3_L.performed += ReturnToMenu;
    }

    private void OnDisable() {
        // unregister input callbacks
        PlayerBehaviour.playerInputActions.UI.Action3_L.performed -= ReturnToMenu;
    }

    private void ReturnToMenu(InputAction.CallbackContext context) {
        if (initialized) {
            AudioManager.Instance.PlaySoundEffect(SoundType.UIPress);
            Close();
        }
    }
}
