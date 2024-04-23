using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    public float speed = 5.5f;
    //public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimmit = 45.0f;

    public bool isCurrentlyMoving = false;

    public StaminaController staminaController;

    public CarryCode playerCarryCode;

    //Sound stuff
    public AudioClip grassSound;
    public AudioClip sandSound;
    public AudioClip rockSound;
    public GameObject GrassTerrain;
    public GameObject SandTerrain;
    public GameObject RockTerrain;

    public string level1Name = "IslandLevel";
    public string level2Name = "CaveLevelV3";
    public string level3Name = "VillageLevel";
    private AudioSource audioSource;


    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;



    [HideInInspector]
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        staminaController = GetComponent<StaminaController>();

        playerCarryCode = GetComponent<CarryCode>();

        characterController = GetComponent < CharacterController>();
        rotation.y = transform.eulerAngles.y;


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Sound schtuff
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            //If AudioSource component is not found, add it
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    private void PlaySound(AudioClip sound)
    {
        if (sound != null)
        {
            audioSource.clip = sound;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                Debug.Log("Sound played: " + sound.name);
            }
        }
    }
    //Update is called once per frame
    void Update()
    {
        //Check the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        //Play sound based on the current scene
        if (isCurrentlyMoving)
        {
            if (sceneName == level1Name)
            {
                PlaySound(grassSound);
                Debug.Log("Playing grassSound");
            }
            else if (sceneName == level2Name)
            {
                PlaySound(sandSound);
                Debug.Log("Playing sandSound");
            }
            else if (sceneName == level3Name)
            {
                PlaySound(rockSound);
                Debug.Log("Playing rockSound");
            }
            else
            {
                Debug.Log("Unknown level: " + sceneName);
            }
        }
        else
        {
            audioSource.Stop();
        }

        
    
        /*
        //Ray cast to check terrain
        //var TerrainRay = Physics.Raycast(RcOg, Vector3.down,LayerMask, 2.0f);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            //print("Terrain ray hit the ground");
            if (hit.collider != null)
            {
                Renderer renderer = hit.collider.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Texture texture1 = renderer.material.mainTexture;
                }
                else
                {
                    Debug.Log("Renderer component isn't found");
                }

            }
            else
            {
                Debug.Log("Collider component not found");
            }
            //Check texture
            Texture texture = hit.collider.GetComponent<Renderer>().material.mainTexture;
            Debug.Log("Raycast hit something: " + hit.collider.name);
            //Play the sound based on texture after making sure texture isn't nothing
            if (texture.name.Contains("Forest"))
            {
                PlaySound(grassSound);
                Debug.Log("Grass detected!");
            }
            else if (texture.name.Contains("Sand"))
            {
                PlaySound(sandSound);
                Debug.Log("Sand detected!");
            }
            else if (texture.name.Contains("Rock"))
            {
                PlaySound(rockSound);
                Debug.Log("Rock detected!");
            }
            else
            {
                Debug.Log("Raycast didn't hit anything.");
            }
        }
        else
        {
            Debug.Log("Raycast hit nothing");
        }
        //Check if it's nothing first

        /*if (TerrainRay == hit){
        print("TerrainRay hit/is hitting terrain");

        }

        */


         
        if (characterController.isGrounded)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (curSpeedX != 0 || curSpeedY != 0)
            {
                isCurrentlyMoving = true;
            }
            else
            {
                isCurrentlyMoving = false;
            }
            

            /*
            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpSpeed;
            }
            */
        }


        if (staminaController.canSprint && Input.GetKey("left shift") && isCurrentlyMoving && !playerCarryCode.hasItem)
        {
            speed = 10.0f;
            staminaController.isSprinting = true;
        }
        else if(staminaController.canSprint && Input.GetKey("left shift") && isCurrentlyMoving && playerCarryCode.hasItem)
        {
            speed = 7.0f;
            staminaController.isSprinting = true;
        }
        else if(isCurrentlyMoving && !playerCarryCode.hasItem)
        {
            speed = 5.5f;
            staminaController.isSprinting = false;
        }
        else
        {
            speed = 3.5f;
            staminaController.isSprinting = false;
        }

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimmit, lookXLimmit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);
        }
        
    }

}
