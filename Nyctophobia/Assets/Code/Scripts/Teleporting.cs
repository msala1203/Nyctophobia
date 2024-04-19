using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporting : MonoBehaviour
{
    //public GameObject Your_Character;
    //public string BeachScene;
    //public string CaveScene;
    //public string VillageScene;
    // Start is called before the first frame update
    public string sceneToLoad;
    public Vector3 destinationPosition;
    
    
    
    private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
            Debug.Log("Player collided with the wall.");
                TeleportPlayer(other.transform);
                //SceneManager.LoadScene(BeachScene);
            }

        }
    
    
    private void TeleportPlayer(Transform player) 
    {
        SceneManager.LoadScene(sceneToLoad);
        player.position = destinationPosition;
    }


    // Update is called once per frame
    /*void Update()
    {
        if (Your_Character.OnCollisionEnter("TeleportBeachLevel")) 
        {
            SceneManager.Load
            LoadScene("BeachLevel", SceneManagement.LoadSceneMode mode = LoadSceneMode.Single);
        }
    }
    */
    
}
