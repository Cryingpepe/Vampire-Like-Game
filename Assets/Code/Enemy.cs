using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animatorControllers; // Array to hold different animator controllers for different enemy types
    public Rigidbody2D target;

    bool isAlive;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator animator;
    SpriteRenderer spriteRenderer;
    WaitForFixedUpdate wait;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isAlive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return; // Skip movement if the enemy is not alive or currently in the hit animation
        }

        Vector2 directionVector = (target.position - rigid.position).normalized; // Calculate the direction towards the target
        Vector2 nextVector = directionVector * speed * Time.fixedDeltaTime; // Calculate the next position based on speed and direction
        rigid.MovePosition(rigid.position + nextVector); // Move the Rigidbody2D to the new position
        rigid.linearVelocity = Vector2.zero; // Reset the linear velocity to prevent continuous movement
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isAlive)
        {
            return;
        }

        spriteRenderer.flipX = target.position.x < rigid.position.x; // Flip the sprite based on the target's position
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>(); // Get the Rigidbody2D of the target player
        isAlive = true; // Set the enemy to be alive when it is enabled
        spriteRenderer.sortingOrder = 2; // Set the sorting order of the sprite renderer to ensure it is rendered above other objects
        animator.SetBool("Dead", false); // Reset the dead animation state
        coll.enabled = true; // Enable the collider to allow interactions
        rigid.simulated = true; // Enable the Rigidbody2D to allow physics interactions
        health = maxHealth; // Reset the health to the maximum health when the enemy is enabled
    }

    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = animatorControllers[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isAlive)
        {
            return; // Ignore collisions with objects that are not bullets
        }

        health -= collision.GetComponent<Bullet>().damage; // Reduce health by the damage of the bullet
        StartCoroutine(KnockBack()); // Start the knockback coroutine

        if (health > 0)
        {
            animator.SetTrigger("Hit"); // Trigger the hit animation
        }
        else
        {
            isAlive = false; // Set the enemy to not alive
            coll.enabled = false; // Disable the collider to prevent further interactions
            rigid.simulated = false; // Stop the Rigidbody2D from simulating physics
            spriteRenderer.sortingOrder = 1; // Reset the sorting order of the sprite renderer
            animator.SetBool("Dead", true); // Trigger the dead animation

            GameManager.instance.kill++; // Increment the kill count in the GameManager
            GameManager.instance.GetExp(); // Call the GetExp method to award experience points
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait; // Wait for the next fixed update
        Vector3 playerPosition = GameManager.instance.player.transform.position; // Get the player's position
        Vector3 playerDirection = transform.position - playerPosition; // Calculate the direction away from the player
        rigid.AddForce(playerDirection.normalized * 3f, ForceMode2D.Impulse); // Apply a knockback force away from the player
    }

    void Dead()
    {
        gameObject.SetActive(false); // Deactivate the enemy game object when it is dead
    }
}
