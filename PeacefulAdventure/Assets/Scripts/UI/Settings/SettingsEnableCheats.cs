using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsEnableCheats : MonoBehaviour
{
    [SerializeField] Toggle cheatsCheckbox;

    public void ToggleEnableCheats() {
        PlayerState.Instance.cheatsEnabled = !PlayerState.Instance.cheatsEnabled;
        cheatsCheckbox.isOn = PlayerState.Instance.cheatsEnabled;
    }

    public void OnToggleValueChanged(bool value) {
        AudioManager.Instance.PlaySoundEffect(SoundType.UIToggle);
        PlayerState.Instance.cheatsEnabled = value;
    }
}
