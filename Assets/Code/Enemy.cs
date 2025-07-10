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
    Animator animator;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }

        Vector2 directionVector = (target.position - rigid.position).normalized; // Calculate the direction towards the target
        Vector2 nextVector = directionVector * speed * Time.fixedDeltaTime; // Calculate the next position based on speed and direction
        rigid.MovePosition(rigid.position + nextVector); // Move the Rigidbody2D to the new position
        rigid.linearVelocity = Vector2.zero; // Reset the linear velocity to prevent continuous movement
    }

    void LateUpdate()
    {
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
        if (!collision.CompareTag("Bullet"))
        {
            return; // Ignore collisions with objects that are not bullets
        }

        health -= collision.GetComponent<Bullet>().damage; // Reduce health by the damage of the bullet

        if (health > 0)
        {

        }
        else
        {
            Dead();
        }
    }

    void Dead()
    {
        gameObject.SetActive(false); // Deactivate the enemy game object when it is dead
    }
}
