using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Settings")]

    public bool isLive;
    public float gameTime;
    public float maxGameTime = 20f; // Maximum game time in seconds

    [Header("Player information")]
    public int playerID; // Unique identifier for the player
    public float health;
    public float maxHealth = 100f; // Maximum health of the player
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 }; // Array to hold the experience required for the next level

    [Header("Game Objects")]
    public Player player;
    public PoolManager poolManager;
    public LevelUp UILevelUp;
    public Result UIresult;
    public Transform UIJoyStick;
    public GameObject enemyCleaner;


    void Awake()
    {
        instance = this; // Ensure there's only one instance of GameManager
        Application.targetFrameRate = 60; // Set the target frame rate for the game
    }

    public void GameStart(int id)
    {
        playerID = id; // Set the player ID
        health = maxHealth; // Initialize player's health to maximum health

        player.gameObject.SetActive(true); // Activate the player object

        UILevelUp.Select(playerID % 2); // Initialize the level-up UI with the player ID
        Resume(); // Start the game by setting isLive to true and resuming time
   
        AudioManager.instance.PlayBGM(true); // Start playing background music
        AudioManager.instance.PlaySFX(AudioManager.SFX.Select); // Start playing select music
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine()); // Start the coroutine to handle game over logic
    }

    IEnumerator GameOverCoroutine()
    {
        isLive = false; // Set the game state to not live

        yield return new WaitForSeconds(0.5f); // Wait for 0.5 second before showing the game over screen

        UIresult.gameObject.SetActive(true); // Activate the game over UI
        UIresult.Lose(); // Show the "You Lose" title
        Stop(); // Stop the game

        AudioManager.instance.PlayBGM(false); // Stop playing background music
        AudioManager.instance.PlaySFX(AudioManager.SFX.Lose); // Play the lose sound effect
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryCoroutine()); // Start the coroutine to handle game victory logic
    }

    IEnumerator GameVictoryCoroutine()
    {
        isLive = false; // Set the game state to not live
        enemyCleaner.SetActive(true); // Activate the enemy cleaner

        yield return new WaitForSeconds(0.5f); // Wait for 0.5 second before showing the game over screen

        UIresult.gameObject.SetActive(true); // Activate the game over UI
        UIresult.Win(); // Show the "You Win" title
        Stop(); // Stop the game

        AudioManager.instance.PlayBGM(false); // Stop playing background music
        AudioManager.instance.PlaySFX(AudioManager.SFX.Win); // Play the win sound effect
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(0); // Reload the current scene to restart the game
    }

    public void GameQuit()
    {
        Application.Quit(); // Quit the application
    }

    void Update()
    {
        if (!isLive)
            return; // If the game is not live, skip the update

        gameTime += Time.deltaTime; // Increment the timer by the time since the last frame

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory(); // If the game time exceeds the maximum, trigger victory
        }
    }

    public void GetExp()
    {
        if (!isLive)
            return; // If the game is not live, skip the experience gain
            
        exp++; // Increment the experience points

        if (exp >= nextExp[Mathf.Min(level, nextExp.Length - 1)]) // Check if the player has enough experience to level up
        {
            level++; // Increase the player's level
            exp = 0; // Reset experience points
            UILevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
        
        UIJoyStick.localScale = Vector3.zero; // Hide the joystick UI
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
        UIJoyStick.localScale = Vector3.one; // Show the joystick UI
    }
}
