using UnityEngine;

public class PlayerPersistence : MonoBehaviour
{
    private static PlayerPersistence instance;

    void Awake()
    {
        ManagePersistence();
    }

    private void ManagePersistence()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Makes this player object persistent across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroys any duplicate player object created on scene load
        }
    }
}
