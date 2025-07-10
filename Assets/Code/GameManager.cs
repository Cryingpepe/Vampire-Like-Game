using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float gameTime;
    public float maxGameTime = 20f; // Maximum game time in seconds
    public Player player;
    public PoolManager poolManager;

    void Awake()
    {
        instance = this; // Ensure there's only one instance of GameManager
    }
    void Update()
    {
        gameTime += Time.deltaTime; // Increment the timer by the time since the last frame

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}
