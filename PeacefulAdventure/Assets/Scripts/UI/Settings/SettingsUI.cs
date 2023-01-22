using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SettingsUI : MonoBehaviour
{
    private bool initialized = false;

    private List<SettingsPropertyUI> properties = new List<SettingsPropertyUI>();
    private int selectedProperty = 0;

    public void Open() {
        PlayerBehaviour.playerInputActions.Player.Disable();
        PlayerBehaviour.playerInputActions.UI.Enable();
        AudioManager.Instance.PlaySoundEffect(SoundType.UIOpen);
        gameObject.TweenAwareEnable();

        // select the first property
        if (!Application.isMobilePlatform) {
            foreach (var prop in properties) {
                prop.Unselect();
            }
            selectedProperty = 0;
            if (properties.Count > 0)
                properties[selectedProperty].Select();
        }
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
        PlayerBehaviour.playerInputActions.UI.Navigation.performed += Navigate;
        PlayerBehaviour.playerInputActions.UI.Action3_L.performed += ReturnToMenu;
    }

    private void OnDisable() {
        // unregister input callbacks
        PlayerBehaviour.playerInputActions.UI.Navigation.performed -= Navigate;
        PlayerBehaviour.playerInputActions.UI.Action3_L.performed -= ReturnToMenu;
    }

    private void Navigate(InputAction.CallbackContext context) {
        if (initialized) {
            Vector2 direction = Utils.ConvertToFourDirections(context.ReadValue<Vector2>());
            if (direction.x == 0) { // up or down
                // select the adjacent settings property
                if (properties.Count > 0) {
                    AudioManager.Instance.PlaySoundEffect(SoundType.UIPress);
                    properties[selectedProperty].Unselect();
                    selectedProperty = Utils.Wrap(selectedProperty - (int)direction.y, 0, properties.Count - 1);
                    properties[selectedProperty].Select();
                }
            } else if (direction.y == 0) { // left or right
                AudioManager.Instance.PlaySoundEffect(SoundType.UIToggle);
                // change value of the currently selected settings property
                if (properties.Count > 0) {
                    properties[selectedProperty].ChangeValue(direction);
                }
            }
        }
    }

    private void ReturnToMenu(InputAction.CallbackContext context) {
        if (initialized) {
            AudioManager.Instance.PlaySoundEffect(SoundType.UIPress);
            Close();
        }
    }

    private void Awake() {
        GetComponentsInChildren<SettingsPropertyUI>(properties);
    }
}
