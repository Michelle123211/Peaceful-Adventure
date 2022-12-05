using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSystem
{
    public delegate void OnExperiencePointsChanged();
    public OnExperiencePointsChanged onExperiencePointsChangedCallback;
    public delegate void OnLevelChanged();
    public OnLevelChanged onLevelChangedCallback;


    public AnimationCurve experienceNeeded;
    [Min(1)]
    public int MaxLevel = 30;
    public int MaxExperience = 2700;

    public int Level { get; private set; } = 0;
    public int Experience { get; private set; } = 0;

    public void UpdateExperience(int delta) {
        Debug.Log($"Current level: {Level}");
        Debug.Log($"Current XP: {Experience}");
        Experience += delta;
        if (Experience >= MaxExperience) {
            Experience = MaxExperience;
        }
        if (onExperiencePointsChangedCallback != null)
            onExperiencePointsChangedCallback.Invoke();
        Debug.Log($"XP increased by {delta} to {Experience}");
    }

    public int GetExperienceNeededForLevel(int level) {
        return (int)(experienceNeeded.Evaluate(level / (float)MaxLevel) * MaxExperience);
    }

    public bool TryLevelUp() {
        if (CanLevelUp()) {
            Debug.Log($"Leveled up from {Level} to {Level + 1}");
            Level++;
            if (onLevelChangedCallback != null)
                onLevelChangedCallback.Invoke();
            return true;
        } else {

        }
        Debug.Log($"Not enough XP to level up from {Level} to {Level + 1}.");
        return false;
    }

    public bool CanLevelUp() {
        if (Level >= MaxLevel) {
            Debug.Log("Already at max level.");
            return false;
        }
        int nextLevelExperience = GetExperienceNeededForLevel(Level + 1);
        Debug.Log($"Current XP: {Experience}");
        Debug.Log($"XP needed: {nextLevelExperience}");
        if (Experience >= nextLevelExperience) {
            return true;
        } else {
            return false;
        }
    }
}
