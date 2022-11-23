using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    private static PlayerState instance;
    public static PlayerState Instance {
        get {
            if (instance != null) return instance;
            instance = GameObject.FindObjectOfType<PlayerState>();
            if (instance == null) {
                GameObject ps = new GameObject();
                ps.name = "PlayerState";
                instance = ps.AddComponent<PlayerState>();
            }
            GameObject.DontDestroyOnLoad(instance);
            return instance;
        }
    }

    [SerializeField] private float maxHealth = 100;
    public float CurrentHealth { get; private set; }

    public Stat attackDamage = new Stat(20f);
    //public float attackDamage = 20;
    [Tooltip("After how many seconds the player can attack again")]
    public Stat attackCooldown = new Stat(0.5f);
    //public float attackCooldown = 0.5f;

    public AnimationCurve experienceNeeded;
    [Min(1)]
    public int MaxLevel = 30;
    public int MaxExperience = 2700;

    public int Level { get; private set; } = 0;
    public int Experience { get; private set; } = 0;

    [Tooltip("Number of slots in the inventory")]
    [SerializeField] private int inventorySlots = 18;
    public int InventorySlots { get => inventorySlots; }
    public Inventory inventory;

    public void UpdateHealth(float delta) {
        CurrentHealth += delta;
        if (CurrentHealth < 0) CurrentHealth = 0;
        if (CurrentHealth > maxHealth) CurrentHealth = maxHealth;
    }

    public void UpdateExperience(int delta) {
        if (Level >= MaxLevel) return;
        Experience += delta;
        int nextLevelExperience = GetExperienceNeededForLevel(Level + 1);
        // level up if needed
        while (nextLevelExperience - Experience <= 0 && Level < MaxLevel) {
            Level++;
            nextLevelExperience = GetExperienceNeededForLevel(Level + 1);
        }
        Debug.Log($"Current level: {Level}");
        Debug.Log($"Current XP: {Experience}");
        Debug.Log($"XP needed: {nextLevelExperience}");
    }

    public int GetExperienceNeededForLevel(int level) {
        return (int)(experienceNeeded.Evaluate(level / (float)MaxLevel) * MaxExperience);
    }

    private void Start() {
        CurrentHealth = maxHealth;
        inventory = new Inventory(inventorySlots);
    }

}
