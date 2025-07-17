using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Settings")]
    public float gameTime;
    public float maxGameTime = 20f; // Maximum game time in seconds

    [Header("Player information")]
    public int health;
    public int maxHealth = 100; // Maximum health of the player
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 }; // Array to hold the experience required for the next level

    [Header("Game Objects")]
    public Player player;
    public PoolManager poolManager;

    void Awake()
    {
        instance = this; // Ensure there's only one instance of GameManager
    }

    void Start()
    {
        health = maxHealth; // Initialize player's health to maximum health
    }

    void Update()
    {
        gameTime += Time.deltaTime; // Increment the timer by the time since the last frame

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    { 
        exp++; // Increment the experience points

        if (exp >= nextExp[level]) // Check if the player has enough experience to level up
        {
            level++; // Increase the player's level
            exp = 0; // Reset experience points
        }
    }
}
