using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimation))]
public class SkeletonBehaviour : EnemyBehaviour, ISaveable<SkeletonState> {

    private PositionID ID;

    public bool IsFollowing { get; set; }

    protected override void ComputeMovement() {
        movement = Vector2.zero;
        if (this.IsFollowing && !this.player.isDead) {
            Vector3 dir = this.player.transform.position - this.transform.position;
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                movement.x = dir.x / Mathf.Abs(dir.x);
            else
                movement.y = dir.y / Mathf.Abs(dir.y);
        }
    }

    public PositionID GetID() {
        return this.ID;
    }

    public void LoadState(SkeletonState model) {
        if (model.isDead) {
            Destroy(gameObject);
            return;
        }
        transform.position = model.position;
        this.currentHealth = model.currentHealth;
    }

    public SkeletonState SaveState() {
        return new SkeletonState { position = transform.position, isDead = this.isDead, currentHealth = this.currentHealth };
    }

    protected override void Start() {
        base.Start();
        this.ID = new PositionID { x = (int)transform.position.x, y = (int)transform.position.y };
    }

    //protected override void Update() {
    //    base.Update();
    //}

    //protected override void FixedUpdate()
    //{
    //    base.FixedUpdate();
    //}

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}

public struct SkeletonState {
    public Vector2 position;
    public bool isDead;
    public float currentHealth;
}
