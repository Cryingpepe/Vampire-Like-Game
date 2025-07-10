using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public SpawnData[] spawnData; // Array to hold spawn data for different enemy types
    int level;
    float timer;


    void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime; // Increment the timer by the time since the last frame
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0f;
            Spawn(); // Spawn an enemy every 0.2 seconds
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.poolManager.GetObject(0); // Get an enemy object from the pool based on the current level
        enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position; // Set the enemy's position to a random spawn point
        enemy.GetComponent<Enemy>().Init(spawnData[level]); // Initialize the enemy with the spawn data for the current level
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}
