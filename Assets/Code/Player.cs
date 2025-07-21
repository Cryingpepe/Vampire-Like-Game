using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public Vector2 inputVector;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animatorControllers;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    void OnEnable()
    {
        speed *= Character.Speed; // Adjust speed based on player ID
        animator.runtimeAnimatorController = animatorControllers[GameManager.instance.playerID]; // Set the animator controller based on player ID
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        Vector2 nextVector = inputVector.normalized * speed * Time.fixedDeltaTime; // Calculate the next position based on input and speed
        rigid.MovePosition(rigid.position + nextVector); // Move the Rigidbody2D to the new position
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        animator.SetFloat("Speed", inputVector.magnitude); // Set the animator parameter based on the magnitude of inputVector

        if (inputVector.x != 0)
        {
            spriteRenderer.flipX = inputVector.x < 0; // Flip the sprite based on the direction of movement
        }
    }

    public void OnMove(InputValue value)
    {
        if (!GameManager.instance.isLive)
            return;

        inputVector = value.Get<Vector2>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        GameManager.instance.health -= Time.deltaTime * 10;

        if (GameManager.instance.health <= 0)
        {
            for (int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false); // Deactivate all child objects except the first two
            }

            animator.SetTrigger("Dead"); // Trigger the death animation
            GameManager.instance.GameOver(); // Call the GameOver method in GameManager
        }
    }
}
