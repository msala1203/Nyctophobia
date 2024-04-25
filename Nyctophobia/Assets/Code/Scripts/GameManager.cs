using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Make this GameManager persistent across scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  // Ensure there's only one instance of GameManager
        }
    }

    // Other game management functions can be added here
}
