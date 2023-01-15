using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameAudio", menuName = "Audio/Game Audio")]
public class GameAudio : ScriptableObject
{
    public float musicFadeOutDuration;

    public Audio backgroundMusic;

    public List<SoundEffect> soundEffects;

    private static GameAudio instance;
    public static GameAudio Instance {
        get {
            if (instance != null) return instance;
            instance = GameObject.FindObjectOfType<GameAudio>();
            if (instance == null) {
                instance = Resources.Load<GameAudio>("GameAudio");
            }
            GameObject.DontDestroyOnLoad(instance);
            return instance;
        }
    }

    public SoundEffect GetSound(SoundType soundType) {
        foreach (SoundEffect sound in soundEffects) {
            if (soundType == sound.type) {
                return sound;
            }
        }
        return null;
    }
}

[System.Serializable]
public class Audio {
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1;
}

[System.Serializable]
public class SoundEffect : Audio {
    public SoundType type;
}

public enum SoundType { 
    PickedItem
}
