using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] ProgressBar healthBar;
    [SerializeField] ProgressBar experienceBar;

    public void RefreshHealth() {
        healthBar.SetMaximum(PlayerState.Instance.MaxHealth);
        healthBar.SetCurrent(PlayerState.Instance.CurrentHealth);
    }

    public void RefreshExperience() {
        experienceBar.SetMaximum(PlayerState.Instance.levelSystem.GetExperienceNeededForLevel(PlayerState.Instance.levelSystem.Level + 1));
        experienceBar.SetCurrent(PlayerState.Instance.levelSystem.Experience);
        experienceBar.SetLabel($"LVL {PlayerState.Instance.levelSystem.Level}");
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
