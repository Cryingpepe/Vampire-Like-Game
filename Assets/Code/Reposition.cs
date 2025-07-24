using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D Collider;

    void Awake()
    {
        Collider = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
        {
            return;
        }

        Vector3 playerPosition = GameManager.instance.player.transform.position;
        Vector3 myPosition = transform.position;


        switch (transform.tag)
        {
            case "Ground":

                float diffX = playerPosition.x - myPosition.x;
                float diffY = playerPosition.y - myPosition.y;
                float directionX = diffX < 0 ? -1 : 1;
                float directionY = diffY < 0 ? -1 : 1;

                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * directionX * 40);
                }
                else if (diffY > diffX)
                {
                    transform.Translate(Vector3.up * directionY * 40);
                }

                break;

            case "Enemy":
                if (Collider.enabled)
                { 
                    Vector3 distance = playerPosition - myPosition;
                    Vector3 randomDistance = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.Translate(randomDistance + distance * 2);
                }

                break;
        }
    }
}
