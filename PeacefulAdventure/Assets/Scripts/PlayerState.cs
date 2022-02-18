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

    private int level = 1;
    private float experience = 0;
    private float experienceStep = 100;

    [Tooltip("Number of slots in the inventory")]
    [SerializeField] private int inventorySlots = 18;
    public Inventory inventory;

    public void UpdateHealth(float delta) {
        CurrentHealth += delta;
        if (CurrentHealth < 0) CurrentHealth = 0;
        if (CurrentHealth > maxHealth) CurrentHealth = maxHealth;
    }

    public void UpdateExperience(float delta) {
        experience += delta;
        if (experience > experienceStep * level) {
            experience -= experienceStep * level;
            level++;
        }
    }

    private void Start() {
        CurrentHealth = maxHealth;
        inventory = new Inventory(inventorySlots);
    }

}
