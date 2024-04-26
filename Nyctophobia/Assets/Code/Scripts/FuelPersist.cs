using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPersist : MonoBehaviour
{
    private static FuelPersist instance;

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
