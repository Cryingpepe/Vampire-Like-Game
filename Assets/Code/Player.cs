using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public Vector2 inputVector;
    public float speed;
    public Scanner scanner;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector2 nextVector = inputVector.normalized * speed * Time.fixedDeltaTime; // Calculate the next position based on input and speed
        rigid.MovePosition(rigid.position + nextVector); // Move the Rigidbody2D to the new position
    }

    void LateUpdate()
    {
        animator.SetFloat("Speed", inputVector.magnitude); // Set the animator parameter based on the magnitude of inputVector

        if (inputVector.x != 0)
        {
            spriteRenderer.flipX = inputVector.x < 0; // Flip the sprite based on the direction of movement
        }
    }

    public void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }
}
