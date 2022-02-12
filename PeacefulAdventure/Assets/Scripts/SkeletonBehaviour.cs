using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimation))]
public class SkeletonBehaviour : EnemyBehaviour
{
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

    //protected override void Start() {
    //    base.Start();
    //}

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
