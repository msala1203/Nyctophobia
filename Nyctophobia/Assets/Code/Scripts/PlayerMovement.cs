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

    public bool isCurrentlyMoving = false;

    public StaminaController staminaController;

    public CarryCode playerCarryCode;

    public GameObject GrassTerrain;
    public GameObject SandTerrain;

   


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
            //print("Terrain ray hit the ground");

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
