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
    if(canPickUp == true) // if you enter the collider of the object
    {
        // If left mouse button is held down
        if (Input.GetMouseButton(0))
        {
            print("picked up Object");
            if (!hasItem)
            {
                PickUpObject();
            }
        }
        else // Left mouse button released
        {
            print("dropped Object");
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

    //ObjectIwantToPickUp drops a little bit off infront of the player

    ObjectIwantToPickUp.transform.parent = null; // make the object not be a child of the hands
    hasItem = false;
}



    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        if(other.gameObject.tag == "PickableObject") //on the object you want to pick up set the tag to be anything, in this case "object"
        {
            canPickUp = true;  //set the pick up bool to true
            ObjectIwantToPickUp = other.gameObject; //set the gameobject you collided with to one you can reference
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canPickUp = false; //when you leave the collider set the canpickup bool to false
   
    }
}
