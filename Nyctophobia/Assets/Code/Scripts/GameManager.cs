using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public AudioSource audioSource; // Attach the AudioSource component in the Inspector

    // Declare individual AudioClip variables for each scene
    public AudioClip ambientClipIsland;
    public AudioClip ambientClipCave;
    public AudioClip ambientClipVillage;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
        // Ensure the AudioSource component is enabled
        if (!audioSource.enabled)
        {
            audioSource.enabled = true;
        }

        //print("On load AudioSource: " + audioSource.enabled);

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayAudioBasedOnScene(scene.name);
    }

    private void PlayAudioBasedOnScene(string sceneName)
    {
        switch (sceneName)
        {
            case "IslandLevel":
                audioSource.clip = ambientClipIsland;
                break;
            case "CaveLevelV3":
                audioSource.clip = ambientClipCave;
                break;
            case "VillageLevel":
                audioSource.clip = ambientClipVillage;
                break;
            default:
                return; // Optionally handle unexpected scenes
        }
        Debug.Log("GameObject active state: " + gameObject.activeSelf);
        Debug.Log("AudioSource enabled state: " + audioSource.enabled);
        Debug.Log("Assigned AudioClip: " + audioSource.clip);


        audioSource.Play();
        audioSource.loop = true;
        Debug.Log("Playing audio for: " + sceneName);
    }

    void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    

}
