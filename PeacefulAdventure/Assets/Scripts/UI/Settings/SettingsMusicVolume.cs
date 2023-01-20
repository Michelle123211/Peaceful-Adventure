using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMusicVolume : MonoBehaviour {
    [SerializeField] Slider volumeSlider;
    private float currentVolume = 1f;
    public void IncreaseMusicVolume() {
        currentVolume = Mathf.Clamp01(currentVolume + 0.1f);
        AudioManager.Instance.ChangeMusicVolume(currentVolume);
        volumeSlider.value = currentVolume;
    }

    public void DecreaseMusicVolume() {
        currentVolume = Mathf.Clamp01(currentVolume - 0.1f);
        AudioManager.Instance.ChangeMusicVolume(currentVolume);
        volumeSlider.value = currentVolume;
    }

    public void OnSliderValueChanged(float value) {
        currentVolume = value;
        AudioManager.Instance.ChangeMusicVolume(currentVolume);
    }

    public void Awake() {
        volumeSlider.value = currentVolume;
    }
}
