using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] ProgressBar healthBar;
    [SerializeField] ProgressBar experienceBar;

    public void RefreshHealth() {
        healthBar.RefreshValues(PlayerState.Instance.CurrentHealth, PlayerState.Instance.MaxHealth);
    }

    public void RefreshExperience() {
        int level = PlayerState.Instance.levelSystem.Level;
        experienceBar.RefreshValues(
            PlayerState.Instance.levelSystem.Experience,
            PlayerState.Instance.levelSystem.GetExperienceNeededForLevel(level + 1),
            $"LVL {level}"
        );
    }

    private void Start() {
        PlayerState.Instance.onHealthChangedCallback += RefreshHealth;
        PlayerState.Instance.levelSystem.onExperiencePointsChangedCallback += RefreshExperience;
        PlayerState.Instance.levelSystem.onLevelChangedCallback += RefreshExperience;
        RefreshHealth();
        RefreshExperience();
    }

    private void OnDestroy() {
        PlayerState.Instance.onHealthChangedCallback -= RefreshHealth;
        if (PlayerState.Instance.levelSystem != null) {
            PlayerState.Instance.levelSystem.onExperiencePointsChangedCallback -= RefreshExperience;
            PlayerState.Instance.levelSystem.onLevelChangedCallback -= RefreshExperience;
        }
    }
}
