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
                instance.musicSource.loop = true;
            }
            GameObject.DontDestroyOnLoad(instance);
            return instance;
        }
    }

    private GameAudio gameAudio;

    private AudioSource soundEffectSource;
    private AudioSource musicSource;

    private Audio nextMusic;

    public void PlaySoundEffect(SoundType soundType, float volume = 1) {
        Audio sound = gameAudio.GetSound(soundType);
        if (sound != null) {
            Debug.Log($"Playing sound effect {soundType}.");
            soundEffectSource.PlayOneShot(sound.clip, sound.volume * volume);
        }
    }

    public void PlaySceneMusic(string sceneName) {
        Debug.Log($"Starting to play music for the scene {sceneName}");
        Audio music = gameAudio.GetMusic(sceneName);
        if (music == null) {
            Debug.Log("No music.");
            StopMusic();
            return;
        }
        if (musicSource.isPlaying) {
            if (nextMusic.clip == music.clip) {
                // do nothing, just continue playing
                Debug.Log("No change in music.");
            } else {
                // change the music
                Debug.Log("Changing music.");
                musicSource.DOKill();
                nextMusic = music;
                musicSource.DOFade(0, gameAudio.musicFadeOutDuration)
                    .OnComplete(() => {
                        PlayMusic();
                    });
            }
        } else {
            // start playing the assigned music
            Debug.Log("Starting music.");
            nextMusic = music;
            PlayMusic();
        }
    }

    private void PlayMusic() {
        Debug.Log($"Playing music {nextMusic.clip.name}.");
        musicSource.volume = nextMusic.volume;
        musicSource.clip = nextMusic.clip;
        musicSource.Play();
    }

    private void StopMusic() {
        // fade out of the currently played music clip
        if (musicSource.isPlaying) {
            musicSource.DOKill();
            musicSource.DOFade(0, gameAudio.musicFadeOutDuration)
                .OnComplete(() => musicSource.Stop());
        }
    }

    private void Awake() {
        gameAudio = GameAudio.Instance;
    }


}
