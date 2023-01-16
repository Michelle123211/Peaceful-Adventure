using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameAudio", menuName = "Audio/Game Audio")]
public class GameAudio : ScriptableObject
{
    public float musicFadeOutDuration;

    public List<SoundEffect> soundEffects;
    [Tooltip("The music corresponding to the first satisfied condition from the top will be played.")]
    public List<SceneMusic> sceneMusics;

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

    public Audio GetSound(SoundType soundType) {
        foreach (SoundEffect sound in soundEffects) {
            if (soundType == sound.type) {
                return sound;
            }
        }
        return null;
    }

    public Audio GetMusic(string sceneName) {
        SceneMusic returnValue = null;
        foreach (SceneMusic music in sceneMusics) {
            switch (music.sceneNameCondition) {
                case StringCondition.Contains:
                    if (sceneName.Contains(music.conditionValue))
                        return music;
                    break;
                case StringCondition.Equals:
                    if (sceneName == music.conditionValue)
                        return music;
                    break;
                case StringCondition.Default:
                    if (returnValue == null)
                        returnValue = music;
                    break;
            }
        }
        return returnValue;
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
    PickedItem,
    Attack,
    Damage,
    EnemyDeath,
    SceneTransition,
    Step
}

[System.Serializable]
public class SceneMusic : Audio {
    public StringCondition sceneNameCondition;
    public string conditionValue;
}

public enum StringCondition { 
    Contains,
    Equals,
    Default
}
