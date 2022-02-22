using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestBehaviour : Interactable
{
    public List<Item> items = new List<Item>(5);
    [Tooltip("Time in seconds between start of animation and UI appearing")]
    [SerializeField] private float lag = 0.4f;

    Animator animator;
    bool isOpen = false;

    public void Close() {
        OnInteraction(new InputAction.CallbackContext());
    }

    public void RemoveItem(Item item) {
        for (int i = 0; i < this.items.Count; ++i) {
            if (this.items[i] == item) {
                this.items[i] = null;
            }
        }
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
        animator.SetBool("IsOpen", true);
        // wait for a moment to allow the animation to finish
        yield return new WaitForSeconds(this.lag);
        // open UI
        Utils.FindObject<ChestUI>()[0].Open(this);
    }

    public IEnumerator CloseChest() {
        // close UI
        Utils.FindObject<ChestUI>()[0].Close();
        // wait for a moment to allow UI to disappear
        yield return new WaitForSeconds(this.lag);
        animator.SetBool("IsOpen", false);
    }

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        PlayerBehaviour.playerInputActions.Chest.Close.performed += OnInteraction;
    }

    void OnDestroy() {
        PlayerBehaviour.playerInputActions.Chest.Close.performed -= OnInteraction;
    }
}
