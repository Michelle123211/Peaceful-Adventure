using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public static PlayerStats instance;

    public float maxHealth = 100;
    public float currentHealth;

    private int level = 1;
    private float experience = 0;
    private float experienceStep = 100;

    public float attackDamage = 20;
    [Tooltip("After how many seconds the player can attack again")]
    public float attackCooldown = 0.5f;
    private float attackMultiplier = 1f;

    private int coins = 0;

    private bool isVulnerable = true;

    public void UpdateHealth(float delta) {
        currentHealth += delta;
        if (currentHealth < 0) currentHealth = 0;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    public void UpdateExperience(float delta) {
        experience += delta;
        if (experience > experienceStep * level) {
            experience -= experienceStep * level;
            level++;
        }
    }

    void Awake() {
        if (PlayerStats.instance != null) {
            Destroy(gameObject);
            return;
        }
        PlayerStats.instance = this;
        DontDestroyOnLoad(gameObject);

        currentHealth = maxHealth;
    }

}
