using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCode : MonoBehaviour
{
    bool hasMotor = false;
    bool hasFuel = false;
    bool isPatched = false;

    string nameOfHeldObject = "none";

    public GameObject Motor;
    public GameObject Fuel;
    public GameObject Metal_Sheet;

    void Start()
    {
        // Initialize variables
        hasMotor = false;
        hasFuel = false;
        isPatched = false;

        Motor.SetActive(false);
        Fuel.SetActive(false);
        Metal_Sheet.SetActive(false);
    }

    void Update()
    {
        if (nameOfHeldObject == "Motor")
        {
            Motor.SetActive(true);
            hasMotor = true;
            nameOfHeldObject = "none";
        }
        else if (nameOfHeldObject == "Fuel")
        {
            Fuel.SetActive(true);
            hasFuel = true;
            nameOfHeldObject = "none";
        }
        else if (nameOfHeldObject == "Metal_Sheet")
        {
            Metal_Sheet.SetActive(true);
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
            if (other.tag == "PickableObject")
            {
                nameOfHeldObject = other.gameObject.name;
                Destroy(other.gameObject);
            }
    }
}
