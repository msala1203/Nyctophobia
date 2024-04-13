using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    public float speed = 5.5f;
    //public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimmit = 45.0f;
    public GameObject GrassTerrain;
    public GameObject SandTerrain;

    public float maxStamina = 100f;
    public float currentStamina = 0f;
    public float staminaUseRate = 10f;
    public float staminaRegainRate = 5f;

    public bool canSprint = true;


    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;



    [HideInInspector]
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent < CharacterController>();
        rotation.y = transform.eulerAngles.y;


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Ray cast Origin
        var RcOg = (0, 0, 0);
        //Ray cast to check terrain
        //var TerrainRay = Physics.Raycast(RcOg, Vector3.down,LayerMask, 2.0f);
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, 25f, LayerMask.GetMask("Default")))
        {
            print("Terrain ray hit the ground");

        }
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
          
            /*
            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpSpeed;
            }
            */
        }


        if (Input.GetKey("left shift") && canSprint)
        {
          
            speed = 10.0f;

            /*
            currentStamina += staminaUseRate;

            if (currentStamina >= maxStamina)
            {
                print("Ran out of stamina");
                canSprint = false;
            }
            /**/

        }
        else
        {
            
            speed = 5.5f;
            /*
            if (currentStamina > 0)
            {
                print("Recovered stamina");
                currentStamina -= staminaRegainRate;
            }
            else
            {
                print("Recovered stamina");
                currentStamina = 0;
                canSprint = true;
            }
            /**/
                
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
