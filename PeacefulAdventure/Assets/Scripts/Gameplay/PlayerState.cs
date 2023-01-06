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
            instance.Initialize();
            GameObject.DontDestroyOnLoad(instance);
            return instance;
        }
    }

    [field:SerializeField] public int MaxHealth { get; private set; } = 100;
    public int CurrentHealth { get; private set; }

    public delegate void OnHealthChanged();
    public OnHealthChanged onHealthChangedCallback;


    public Stat attackDamage = new Stat(20f);
    //public float attackDamage = 20;
    [Tooltip("After how many seconds the player can attack again")]
    public Stat attackCooldown = new Stat(0.5f);
    //public float attackCooldown = 0.5f;

    [Tooltip("Number of slots in the inventory")]
    [SerializeField] private int inventorySlots = 12;
    public int InventorySlots { get => inventorySlots; }
    public Inventory inventory;


    public LevelSystem levelSystem;

    private static PlayerStateData initialState; // different after completing the tutorial

    public bool tutorialCompleted = false;


    public void UpdateHealth(int delta) {
        CurrentHealth += delta;
        if (CurrentHealth < 0) CurrentHealth = 0;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        onHealthChangedCallback?.Invoke();
    }

    public void UpdateMaxHealth(int newValue) {
        int delta = newValue - MaxHealth;
        MaxHealth = newValue;
        UpdateHealth(delta);
    }

    public static void ResetCompletely() {
        if (instance != null) {
            instance.MaxHealth = 100;
            instance.CurrentHealth = instance.MaxHealth;
            instance.attackDamage = new Stat(20f);
            instance.attackCooldown = new Stat(0.5f);
            instance.inventorySlots = 12;
            instance.inventory = new Inventory(instance.inventorySlots);
            instance.levelSystem.ResetCompletely();
            Debug.Log("PlayerState reset completely.");
        }
    }

    public static void SetCurrentStateAsInitial() {
        initialState = SaveState();
        Debug.Log("Current state was set as initial.");
    }

    public static void ResetToInitialState() {
        LoadState(initialState);
        Debug.Log("PlayerState reset to the initial state.");
    }

    public static void LoadState(PlayerStateData stateData) {
        Instance.MaxHealth = stateData.maxHealth;
        Instance.CurrentHealth = stateData.currentHealth;
        Instance.attackDamage = new Stat(stateData.attackDamage.Value);
        Instance.attackCooldown = new Stat(stateData.attackCooldown.Value);
        Instance.inventorySlots = stateData.inventorySlots;
        Instance.levelSystem.LoadState(stateData.maxLevel, stateData.maxExperience, stateData.level, stateData.experience);
        // deep copy of the inventory
        Instance.inventory = stateData.inventory.Copy();
        Debug.Log("PlayerState state loaded.");
    }

    public static PlayerStateData SaveState() {
        if (Instance != null && Instance.levelSystem != null && Instance.inventory != null) {
            PlayerStateData result = new PlayerStateData {
                maxHealth = Instance.MaxHealth,
                currentHealth = Instance.CurrentHealth,
                attackDamage = Instance.attackDamage,
                attackCooldown = Instance.attackCooldown,
                inventorySlots = Instance.InventorySlots,
                maxLevel = Instance.levelSystem.maxLevel,
                maxExperience = Instance.levelSystem.maxExperience,
                level = Instance.levelSystem.Level,
                experience = Instance.levelSystem.Experience
            };
            // deep copy of the inventory
            result.inventory = Instance.inventory.Copy();
            Debug.Log("PlayerState saved.");
            return result;
        } else {
            return null;
        }
    }

    private void Initialize() {
        CurrentHealth = MaxHealth;
        onHealthChangedCallback?.Invoke(); // update the UI
        inventory = new Inventory(inventorySlots);
        initialState = SaveState();
    }

}


public class PlayerStateData {
    public int maxHealth = 100;
    public int currentHealth = 100;
    public Stat attackDamage = new Stat(20f);
    public Stat attackCooldown = new Stat(0.5f);
    public int inventorySlots = 18;
    public Inventory inventory;
    public int maxLevel = 30;
    public int maxExperience = 2700;
    public int level = 0;
    public int experience = 0;

    public PlayerStateData() {
        inventory = new Inventory(inventorySlots);
    }
}