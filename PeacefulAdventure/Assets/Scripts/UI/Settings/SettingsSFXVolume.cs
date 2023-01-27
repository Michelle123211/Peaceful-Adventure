using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSFXVolume : MonoBehaviour
{
    [Tooltip("Slider used to control the sound effects volume.")]
    [SerializeField] Slider volumeSlider;
    private float currentVolume = 1f;
    public void IncreaseSFXVolume() {
        currentVolume = Mathf.Clamp01(currentVolume + 0.1f);
        AudioManager.Instance.ChangeSoundEffectVolume(currentVolume);
        volumeSlider.value = currentVolume;
    }

    public void DecreaseSFXVolume() {
        currentVolume = Mathf.Clamp01(currentVolume - 0.1f);
        AudioManager.Instance.ChangeSoundEffectVolume(currentVolume);
        volumeSlider.value = currentVolume;
    }

    public void OnSliderValueChanged(float value) {
        AudioManager.Instance.PlaySoundEffect(SoundType.UIToggle);
        currentVolume = value;
        AudioManager.Instance.ChangeSoundEffectVolume(currentVolume);
    }

    private void Start() {
        currentVolume = AudioManager.Instance.GetSoundEffectVolume();
        volumeSlider.value = currentVolume;
    }
}
