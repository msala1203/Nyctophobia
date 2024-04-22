using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCode : MonoBehaviour
{
    bool isBroken = true;
    bool hasMotor = false;
    bool hasFuel = false;
    bool isPatched = false;

    //public Transform potentialChild;
    string nameOfHeldObject = "none";

    public GameObject playerHand;

    // Start is called before the first frame update
    void Start()
    {
        isBroken = true;
        hasMotor = false;
        hasFuel = false;
        isPatched = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (nameOfHeldObject == "Motor")
        {
            hasMotor = true;
            nameOfHeldObject = "none";
        }
        else if (nameOfHeldObject == "Fuel")
        {
            hasFuel = true;
            nameOfHeldObject = "none";
        }
        else if (nameOfHeldObject == "Metal_Sheet")
        {
            isPatched = true;
            nameOfHeldObject = "none";
        }

        if (hasMotor && hasFuel && isPatched)
        {
            print("Boat Fixed");
            isBroken = false;
        }


    }

    private void OnTriggerEnter(Collider other) // to see when the boat has an object in its range
    {
        if (other.gameObject.tag == "PickableObject")
        {
            Transform otherTransform = other.gameObject.transform;
            if (otherTransform.parent != playerHand.transform)
            {
                nameOfHeldObject = other.gameObject.name;
                Destroy(other.gameObject);
            }
        }



    }
    
    /*
    private void OnTriggerExit(Collider other)
    {

    }
    */
}
