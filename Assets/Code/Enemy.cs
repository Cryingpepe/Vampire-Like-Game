using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    bool isAlive = true;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    }
}
