using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public PoolManager poolManager;
    
    void Awake()
    {
        instance = this; // Ensure there's only one instance of GameManager
    }
}
