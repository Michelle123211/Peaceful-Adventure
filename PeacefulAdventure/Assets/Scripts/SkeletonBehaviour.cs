//#define VELOCITY_MOVEMENT
#define FORCE_MOVEMENT

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimation))]
public class SkeletonBehaviour : MonoBehaviour
{
#if VELOCITY_MOVEMENT
    public float speed = 2.5f;
#endif
#if FORCE_MOVEMENT
    public float force = 25f;
#endif

    private PlayerBehaviour player;

    Rigidbody2D rb;
    CharacterAnimation animator;
    SpriteRenderer spriteRenderer;

#if FORCE_MOVEMENT
    Vector2 movement;
#endif

    [Tooltip("Attack every x seconds")]
    public float attackCooldown = 3;
    private bool canAttack = true;
    private float attackDelta = 0;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<CharacterAnimation>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerBehaviour>();
    }

    private void Update() {
        Vector3 dir = this.player.transform.position - this.transform.position;
        if (canAttack) {
            if (dir.magnitude < 1.5) {
                canAttack = false;
                attackDelta = 0;
                Attack();
            }
        } else {
            attackDelta += Time.deltaTime;
            if (attackDelta > attackCooldown) canAttack = true;
        }
        // sorting layers
        if (dir.magnitude < 4) {
            if (dir.y < 0)
                spriteRenderer.sortingLayerName = "EnemyBack";
            else
                spriteRenderer.sortingLayerName = "EnemyFront";
        }
    }

    void FixedUpdate()
    {
        Vector2 movement = Vector2.zero;
        Vector3 dir = this.player.transform.position - this.transform.position;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            movement.x = dir.x / Mathf.Abs(dir.x);
        else
            movement.y = dir.y / Mathf.Abs(dir.y);

        Debug.Log(dir.magnitude);

#if FORCE_MOVEMENT
        rb.AddForce(movement * force);
#endif
#if VELOCITY_MOVEMENT
        rb.velocity = movement * speed;
#endif

        animator.Move(movement);
    }

    private void Attack() {
        Debug.Log("Attack!");
        animator.Attack();
    }
}
