using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using DG.Tweening.Core;

public class AudioManager : MonoBehaviour
{

    private static AudioManager instance;
    public static AudioManager Instance {
        get {
            if (instance != null) return instance;
            instance = GameObject.FindObjectOfType<AudioManager>();
            if (instance == null) {
                GameObject ps = new GameObject();
                ps.name = "AudioManager";
                instance = ps.AddComponent<AudioManager>();
                instance.soundEffectSource = ps.AddComponent<AudioSource>();
                instance.musicSource = ps.AddComponent<AudioSource>();
            }
            GameObject.DontDestroyOnLoad(instance);
            return instance;
        }
    }

    private GameAudio gameAudio;

    private AudioSource soundEffectSource;
    private AudioSource musicSource;

    public void PlaySoundEffect(SoundType soundType) {
        SoundEffect sound = gameAudio.GetSound(soundType);
        if (sound != null) {
            Debug.Log($"Playing sound effect {soundType}.");
            soundEffectSource.PlayOneShot(sound.clip, sound.volume);
        }
    }

    public void PlayMusic() {
        Debug.Log("Playing music.");
        musicSource.volume = gameAudio.backgroundMusic.volume;
        musicSource.clip = gameAudio.backgroundMusic.clip;
        musicSource.Play();
    }

    public void StopMusic() {
        // fade out of the currently played music clip
        if (musicSource.isPlaying) {
            musicSource.DOFade(0, gameAudio.musicFadeOutDuration)
                .OnComplete(() => musicSource.Stop());
        }
    }

    private void Awake() {
        gameAudio = GameAudio.Instance;
    }


}
