using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryCode : MonoBehaviour
{
    
    bool canPickUp = false;
    GameObject ObjectIwantToPickUp;
    bool hasItem; 
    public GameObject myHands;
    

    private BoxCollider boxCollider;

    void Start()
    {
        canPickUp = false;
        hasItem = false;

        boxCollider = GetComponent<BoxCollider>();

    }


    void Update()
    {
        //print("hasItem Bool: " + hasItem + "\ncanPickUp Bool:" + canPickUp);

        if (canPickUp == true) // if you enter the collider of the object
        {
        
            // If left mouse button is held down
        if (Input.GetMouseButton(0))
        {
            //print("picked up Object" + "hasItem Bool: " + hasItem);
            if (!hasItem)
            {
                PickUpObject();
            }
        }
        else // Left mouse button released
        {
            //print("dropped Object" + "\nhasItem Bool: " + hasItem);
            if (hasItem)
            {
                ReleaseObject();
            }
        }
    }
}

    void PickUpObject()
    {
        ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;   // makes the rigidbody not be acted upon by forces
   
        ObjectIwantToPickUp.transform.position = myHands.transform.position; // sets the position of the object to your hand position
        ObjectIwantToPickUp.transform.parent = myHands.transform; // makes the object become a child of the parent so that it moves with the hands
        hasItem = true;
    }


    void ReleaseObject()
    {
        ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = false; // make the rigidbody work again

        // Raycast to find the position in front of the player
        RaycastHit hit;
        float raycastDistance = 2.0f; // Adjust this value based on your scene scale and player height
        Vector3 raycastOrigin = transform.position + (transform.forward * 2.0f); // Adjust forward offset as needed
        if (Physics.Raycast(raycastOrigin, transform.forward, out hit, raycastDistance))
        {
            // Place the object slightly in front of the hit point to avoid clipping
            float offset = 2.0f; // Adjust this value to control how much in front of the hit point the object is placed
            Vector3 dropPosition = hit.point + (transform.forward * offset);

            // Add an offset above the ground
            float aboveGroundOffset = 2f; // Adjust this value to control how much above the ground the object is placed
            dropPosition.y += aboveGroundOffset;

            // Check if the drop position is not too far from the player, to prevent unrealistic drops
            float maxDropDistance = 6.0f; 
            if (Vector3.Distance(transform.position, dropPosition) <= maxDropDistance)
            {
                ObjectIwantToPickUp.transform.position = dropPosition;
            }
            else
            {
                // If drop position is too far, just drop the object slightly in front of the player
                float upDistance = 2.0f; 
                dropPosition = transform.position + (transform.forward * upDistance);
                ObjectIwantToPickUp.transform.position = dropPosition;
            }
        }
        else
        {
            // If no obstacle detected, just drop the object slightly in front of the player
            float upDistance = 3.0f; // Adjust this value based on your preference
            Vector3 dropPosition = transform.position + (transform.forward * upDistance);
            ObjectIwantToPickUp.transform.position = dropPosition;
        }

        ObjectIwantToPickUp.transform.parent = null; // make the object not be a child of the hands
        hasItem = false;
    }

    /*
    void ReleaseObject()
    {
        ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = false; // make the rigidbody work again
        // Drop the object slightly in front of the player
        //float dropDistance = 2.0f; // Adjust this value to control how far in front of the player the object is dropped
        float upDistance = 3.0f;
        Vector3 dropPosition = transform.position + (transform.forward * upDistance);
        ObjectIwantToPickUp.transform.position = dropPosition;

        ObjectIwantToPickUp.transform.parent = null; // make the object not be a child of the hands
        hasItem = false;
    }
    /**/


    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        if(other.gameObject.tag == "PickableObject" && !hasItem) //on the object you want to pick up set the tag to be anything, in this case "object"
        {
            canPickUp = true;  //set the pick up bool to true
            ObjectIwantToPickUp = other.gameObject; //set the gameobject you collided with to one you can reference
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(!hasItem)
        {
            canPickUp = false; //when you leave the collider set the canpickup bool to false
        }
           
   
    }
}
