using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioSource audioSource;

    public AudioClip ambientClipIsland;
    public AudioClip ambientClipCave;
    public AudioClip ambientClipVillage;

    // State variables for objects and boat components
    public Vector3 motorPosition;
    public Vector3 fuelPosition;
    public Vector3 patchPosition;
    public int motorSceneIndex;
    public int fuelSceneIndex;
    public int patchSceneIndex;

    public bool hasMotor;
    public bool hasFuel;
    public bool isPatched;
    public bool boatRepaired;

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

        LoadGameData();
        if (!audioSource.enabled)
        {
            audioSource.enabled = true;
        }

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
            case "MainMenu":
                audioSource.clip = ambientClipIsland;
                break;
            default:
                return;
        }
        audioSource.Play();
        audioSource.loop = true;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Save and Load methods for game data
    public void SaveGameData()
    {
        SaveObjectData("Motor", motorPosition, motorSceneIndex);
        SaveObjectData("Fuel", fuelPosition, fuelSceneIndex);
        SaveObjectData("Patch", patchPosition, patchSceneIndex);

        PlayerPrefs.SetInt("HasMotor", hasMotor ? 1 : 0);
        PlayerPrefs.SetInt("HasFuel", hasFuel ? 1 : 0);
        PlayerPrefs.SetInt("IsPatched", isPatched ? 1 : 0);
        PlayerPrefs.SetInt("BoatRepaired", boatRepaired ? 1 : 0);

        PlayerPrefs.Save();
    }

    private void SaveObjectData(string objectKey, Vector3 position, int sceneIndex)
    {
        PlayerPrefs.SetFloat(objectKey + "PosX", position.x);
        PlayerPrefs.SetFloat(objectKey + "PosY", position.y);
        PlayerPrefs.SetFloat(objectKey + "PosZ", position.z);
        PlayerPrefs.SetInt(objectKey + "SceneIndex", sceneIndex);
    }

    public void LoadGameData()
    {
        motorPosition = LoadObjectPosition("Motor");
        fuelPosition = LoadObjectPosition("Fuel");
        patchPosition = LoadObjectPosition("Patch");

        motorSceneIndex = PlayerPrefs.GetInt("MotorSceneIndex");
        fuelSceneIndex = PlayerPrefs.GetInt("FuelSceneIndex");
        patchSceneIndex = PlayerPrefs.GetInt("PatchSceneIndex");

        hasMotor = PlayerPrefs.GetInt("HasMotor") == 1;
        hasFuel = PlayerPrefs.GetInt("HasFuel") == 1;
        isPatched = PlayerPrefs.GetInt("IsPatched") == 1;
        boatRepaired = PlayerPrefs.GetInt("BoatRepaired") == 1;
    }

    private Vector3 LoadObjectPosition(string objectKey)
    {
        float x = PlayerPrefs.GetFloat(objectKey + "PosX");
        float y = PlayerPrefs.GetFloat(objectKey + "PosY");
        float z = PlayerPrefs.GetFloat(objectKey + "PosZ");
        return new Vector3(x, y, z);
    }
}
