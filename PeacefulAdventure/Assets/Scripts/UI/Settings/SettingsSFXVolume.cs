using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSFXVolume : MonoBehaviour
{
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

    public void Awake() {
        volumeSlider.value = currentVolume;
    }
}
