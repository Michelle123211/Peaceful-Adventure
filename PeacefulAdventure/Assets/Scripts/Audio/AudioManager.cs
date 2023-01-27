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

    private float masterMusicVolume = 1f;
    private float masterSoundEffectVolume = 1f;

    public void PlaySoundEffect(SoundType soundType, float volume = 1) {
        Audio sound = gameAudio.GetSound(soundType);
        if (sound != null) {
            soundEffectSource.PlayOneShot(sound.clip, sound.volume * volume * masterSoundEffectVolume);
        }
    }

    public void PlaySceneMusic(string sceneName) {
        Audio music = gameAudio.GetMusic(sceneName);
        if (music == null) {
            StopMusic();
            return;
        }
        if (musicSource.isPlaying) {
            if (nextMusic.clip == music.clip) {
                // do nothing, just continue playing
            } else {
                // change the music
                musicSource.DOKill();
                nextMusic = music;
                musicSource.DOFade(0, gameAudio.musicFadeOutDuration)
                    .OnComplete(() => {
                        PlayMusic();
                    });
            }
        } else {
            // start playing the assigned music
            nextMusic = music;
            PlayMusic();
        }
    }

    public float GetMusicVolume() => masterMusicVolume;
    public void ChangeMusicVolume(float volume) {
        // change volume of the currently played music
        if (masterMusicVolume == 0)
            musicSource.volume = nextMusic.volume * volume;
        else
            musicSource.volume = musicSource.volume * (1 / masterMusicVolume) * volume;
        masterMusicVolume = volume;
    }

    public float GetSoundEffectVolume() => masterSoundEffectVolume;
    public void ChangeSoundEffectVolume(float volume) {
        // sound effects are short, no need to change the volume of currently played sound effects
        masterSoundEffectVolume = volume;
    }

    private void PlayMusic() {
        musicSource.volume = nextMusic.volume * masterMusicVolume;
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
