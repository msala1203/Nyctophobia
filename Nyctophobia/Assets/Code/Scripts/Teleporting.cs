using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporting : MonoBehaviour
{

    public string sceneToLoad;
    public string spawnPointName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Teleportation triggered: Player has collided with the teleporter.");
            TeleportPlayer(other.gameObject);
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        // Optionally disable the player's collider to prevent physics interactions during spawn
        Collider playerCollider = player.GetComponent<Collider>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
            Debug.Log("Player collider disabled to prevent physics interactions during teleportation.");
        }

        // Load the new scene and attach a callback to execute once the scene has loaded
        SceneManager.LoadSceneAsync(sceneToLoad).completed += (AsyncOperation op) =>
        {
            Debug.Log($"Scene '{sceneToLoad}' successfully loaded.");

            // Find the spawn point object in the newly loaded scene
            GameObject spawnPoint = GameObject.Find(spawnPointName);
            if (spawnPoint != null)
            {
                player.transform.position = spawnPoint.transform.position;
                Debug.Log("Player teleported to new spawn point.");
            }
            else
            {
                Debug.LogError($"Spawn point '{spawnPointName}' not found in scene '{sceneToLoad}'. Check spawn point naming and presence in the scene.");
            }

            // Re-enable the player's collider
            if (playerCollider != null)
            {
                playerCollider.enabled = true;
                Debug.Log("Player collider re-enabled post-teleportation.");
            }
        };
    }
}
