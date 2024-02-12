using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider entering the stop box is a car
        if (other.CompareTag("Car"))
        {
            Debug.Log("Car trigger.");
            // Stop the car's movement
            CarMovement carMovement = other.GetComponent<CarMovement>();
            if (carMovement != null)
            {
                carMovement.StopMovement();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the collider exiting the stop box is a car
        if (other.CompareTag("Car"))
        {
            // Resume the car's movement
            CarMovement carMovement = other.GetComponent<CarMovement>();
            if (carMovement != null)
            {
                carMovement.ResumeMovement();
            }
        }
    }
}
