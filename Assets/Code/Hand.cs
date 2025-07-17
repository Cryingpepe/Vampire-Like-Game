using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriteRenderer;

    SpriteRenderer player;

    Vector3 rightPosition = new Vector3(0.35f, -0.14f, 0);
    Vector3 rightPositionReverse = new Vector3(-0.15f, -0.14f, 0);
    Quaternion leftRotation = Quaternion.Euler(0, 0, -35);
    Quaternion leftRotationReverse = Quaternion.Euler(0, 0, -135);



    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    void LateUpdate()
    {
        bool isReverse = player.flipX;

        if (isLeft)
        {
            transform.localRotation = isReverse ? leftRotationReverse : leftRotation;
            spriteRenderer.flipY = isReverse;
            spriteRenderer.sortingOrder = isReverse ? 4 : 6;
        }
        else
        {
            transform.localPosition = isReverse ? rightPositionReverse : rightPosition;
            spriteRenderer.flipX = isReverse;
            spriteRenderer.sortingOrder = isReverse ? 6 : 4;
        }
    }
}
