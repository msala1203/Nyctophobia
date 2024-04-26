using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTotal : MonoBehaviour
{
    //Variables
    public GameObject monsterPrefab;

    //5 minute delay
    public float spawnDelay = 10f;
    //1 minute after delay
    public float respawner = 60f;
    //Spawn somewhat close to player
    public float spawnRadius = 10f;
    //MOVE SPEED CHANGE THIS FOR SPEED
    public float moveSpeed = 5f;

    private GameObject player;
    private bool spawned = false;
    private bool spotted = false;
    private float spawnTimer;
    private float respawnTimer;
    private Transform monsterTransform;

    
    void Start()
    {
        //Get all the variables together
        player = GameObject.FindGameObjectWithTag("Player");
        spawnTimer = spawnDelay;
        respawnTimer = respawner;
    }

    
    void Update()
    {
        //If it doesn't exist at this time
        if (!spawned)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                SpawnMonster();
                Debug.Log("Monster spawned!");
            }
        }
        //If it exists, check for being spotted
        else
        {
            if (!spotted)
            {
                //Move towards the player if not spotted
                Vector3 direction = player.transform.position - monsterTransform.position;
                monsterTransform.Translate(direction.normalized * moveSpeed * Time.deltaTime);

                //Monster always faces the player(This is why we could probably use 
                monsterTransform.LookAt(player.transform.position);
                //Check if player is within sight range
                if (Vector3.Distance(player.transform.position, monsterTransform.position) < 2f)
                {
                    spotted = true;
                    Debug.Log("Monster spotted!");
                    //Add code to despawn the monster when spotted by the player
                    
                }
            }
        }
    

    }
    //Spawning in the monster function
    void SpawnMonster()
    {
        //Randomize position near the player
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
        randomDirection += player.transform.position;
        //Make sure monster spawns on the same level y level
        randomDirection.y = 0f;

        //Instantiate the monster prefab
        //Instantiate the monster prefab
        GameObject monsterObject = Instantiate(monsterPrefab, randomDirection, Quaternion.identity);
        monsterTransform = monsterObject.transform;

        //Initial rotation: facing forward
        Quaternion rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 90f, 0f);
        monsterTransform.rotation = rotation;



        spawned = true;
    }
}
