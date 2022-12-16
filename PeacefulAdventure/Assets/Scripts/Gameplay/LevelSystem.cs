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
    public int maxLevel = 30;
    public int maxExperience = 2700;

    public int Level { get; private set; } = 0;
    public int Experience { get; private set; } = 0;

    public void UpdateExperience(int delta) {
        Debug.Log($"Current level: {Level}");
        Debug.Log($"Current XP: {Experience}");
        Experience += delta;
        if (Experience >= maxExperience) {
            Experience = maxExperience;
        }
        onExperiencePointsChangedCallback?.Invoke();
        Debug.Log($"XP increased by {delta} to {Experience}");
    }

    public int GetExperienceNeededForLevel(int level) {
        return (int)(experienceNeeded.Evaluate(level / (float)maxLevel) * maxExperience);
    }

    public bool TryLevelUp() {
        if (CanLevelUp()) {
            Debug.Log($"Leveled up from {Level} to {Level + 1}");
            Level++;
            onLevelChangedCallback?.Invoke();
            return true;
        } else {

        }
        Debug.Log($"Not enough XP to level up from {Level} to {Level + 1}.");
        return false;
    }

    public bool CanLevelUp() {
        if (Level >= maxLevel) {
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

    public void ResetCompletely() {
        maxLevel = 30;
        maxExperience = 2700;
        Level = 0;
        Experience = 0;
        onExperiencePointsChangedCallback?.Invoke();
        onLevelChangedCallback?.Invoke();
        Debug.Log("LevelSystem reset completely.");
    }

    public void LoadState(int maxLevel, int maxExperience, int level, int experience) {
        this.maxLevel = maxLevel;
        this.maxExperience = maxExperience;
        this.Level = level;
        this.Experience = experience;
        onExperiencePointsChangedCallback?.Invoke();
        onLevelChangedCallback?.Invoke();
        Debug.Log("LevelSystem state loaded.");
    }
}
