using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    float timer;

    void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime; // Increment the timer by the time since the last frame

        if (timer > 0.2f)
        {
            timer = 0f;
            Spawn(); // Spawn an enemy every 0.2 seconds
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.poolManager.GetObject(Random.Range(0, GameManager.instance.poolManager.prefabs.Length)); // Get a random enemy from the pool manager
        enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position; // Set the enemy's position to a random spawn point
    }
}
