using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryCode : MonoBehaviour
{
    
    bool canPickUp = false;
    GameObject ObjectIwantToPickUp;
    public bool hasItem; 
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
            if (Input.GetMouseButtonDown(0)) // If left mouse button is clicked
            {
                if (!hasItem)
                {
                    // Pick up the object if not already holding something
                    PickUpObject();
                }
                else
                {
                    // Drop the object if already holding something
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

    /*
   IEnumerator ReleaseObject()
{
    ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = false; // Reactivate physics

    bool validDropPositionFound = false;
    float checkDistance = 3.0f; // Initial distance to drop the object
    float maxDistance = 5.0f; // Maximum distance to check
    while (!validDropPositionFound && checkDistance <= maxDistance)
    {
        Vector3 proposedDropPosition = transform.position + (transform.forward * checkDistance);
        // Check for obstacles using a raycast
        if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, checkDistance))
        {
            // No obstacles, check ground level using a downward raycast
            if (Physics.Raycast(proposedDropPosition, -Vector3.up, out hit))
            {
                proposedDropPosition.y = hit.point.y + 0.5f; // Adjust to be slightly above the ground
                validDropPositionFound = true;
                ObjectIwantToPickUp.transform.position = proposedDropPosition;
                ObjectIwantToPickUp.transform.parent = null; // Detach from player
                hasItem = false;
                break;
            }
        }
        checkDistance += 0.5f; // Increment check distance
        yield return new WaitForSeconds(0.1f); // Wait before next check to reduce performance hit
    }

    if (!validDropPositionFound)
    {
        Debug.Log("No valid position found to drop the object. Try moving to a clearer area.");
        // Optionally, handle the situation, e.g., reattach the object or notify the player
    }
}
    /**/
    
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
