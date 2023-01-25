using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestBehaviour : Interactable, ISaveable<List<InventoryItem>> {
    [Tooltip("Items contained in the chest (if not initialized randomly).")]
    public List<InventoryItem> items = new List<InventoryItem>();
    [Tooltip("Time in seconds between start of animation and UI appearing")]
    [SerializeField] private float lag = 0.4f;
    [Tooltip("A list of items which will be used for random initialization")]
    [SerializeField] private List<Item> itemsToPickRandomly = new List<Item>();

    Animator animator;
    bool isOpen = false;

    public void Close() {
        OnInteraction(new InputAction.CallbackContext());
    }

    public void InitializeItemsRandomly(int count) {
        items.Clear();
        int[] itemCounts = new int[itemsToPickRandomly.Count];
        // how many of each item will be selected
        for (int i = 0; i < count; ++i) {
            itemCounts[Random.Range(0, itemsToPickRandomly.Count - 1)]++;
        }
        // add the corresponding number of items to the chest
        for (int i = 0; i < itemCounts.Length; ++i) {
            if (itemCounts[i] > 0) {
                items.Add(new InventoryItem(itemsToPickRandomly[i], itemCounts[i]));
            }
        }
    }

    [ContextMenu("Fill with random items")]
    public void FillWithRandomItems() {
        InitializeItemsRandomly(Random.Range(2, 4));
    }

    [ContextMenu("Fill with one of each items")]
    public void FillWithOneOfEach() {
        items.Clear();
        // add the corresponding number of items to the chest
        for (int i = 0; i < itemsToPickRandomly.Count; ++i) {
            items.Add(new InventoryItem(itemsToPickRandomly[i], 1));
        }
    }

    public void RemoveItem(Item item, int count = 1) {
        for (int i = 0; i < this.items.Count; ++i) {
            if (this.items[i] != null && this.items[i].item == item) {
                // remove the given count of the given item
                this.items[i].count -= count;
                if (this.items[i].count <= 0) // remove if none are left
                    this.items[i] = null;
                return;
            }
        }
    }

    public void RemoveItem(InventoryItem item) {
        RemoveItem(item.item, item.count);
    }

    protected override void OnInteraction(InputAction.CallbackContext context) {
        this.isOpen = !this.isOpen;
        if (this.isOpen) {
            StartCoroutine(OpenChest());
        } else {
            StartCoroutine(CloseChest());
        }
    }

    public IEnumerator OpenChest() {
        PlayerBehaviour.playerInputActions.UI.Action3_L.performed += OnInteraction;
        animator.SetBool("IsOpen", true);
        AudioManager.Instance.PlaySoundEffect(SoundType.ChestOpen);
        // wait for a moment to allow the animation to finish
        yield return new WaitForSeconds(this.lag);
        // open UI
        Utils.FindObject<ChestUI>()[0].Open(this);
    }

    public IEnumerator CloseChest() {
        PlayerBehaviour.playerInputActions.UI.Action3_L.performed -= OnInteraction;
        // close UI
        Utils.FindObject<ChestUI>()[0].Close();
        // wait for a moment to allow UI to disappear
        yield return new WaitForSeconds(this.lag);
        animator.SetBool("IsOpen", false);
        AudioManager.Instance.PlaySoundEffect(SoundType.ChestClose);
    }

    void Awake() {
        animator = GetComponent<Animator>();
    }

    public PositionID GetID() {
        return new PositionID { x = (int)transform.position.x, y = (int)transform.position.y };
    }

    public List<InventoryItem> SaveState() {
        return this.items;
    }

    public void LoadState(List<InventoryItem> model) {
        this.items = model;
    }
}
