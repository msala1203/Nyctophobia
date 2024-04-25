using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCode : MonoBehaviour
{
    bool hasMotor = false;
    bool hasFuel = false;
    bool isPatched = false;

    string nameOfHeldObject = "none";

    public GameObject playerHand;

    void Start()
    {
        // Initialize variables
        hasMotor = false;
        hasFuel = false;
        isPatched = false;
    }

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
        }
    }

    private void OnTriggerEnter(Collider other)
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
}
