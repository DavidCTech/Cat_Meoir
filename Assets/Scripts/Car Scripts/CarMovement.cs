using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController))] // Make sure there is a CarController component on the GameObject

public class CarMovement : MonoBehaviour
{
    private CarController carController;

    private void Start()
    {
        carController = GetComponent<CarController>();
        StartCoroutine(MoveCar());
    }

    private IEnumerator MoveCar()
    {
        while (true)
        {
            // Move the car forward
            carController.Move(new Vector2(0, 1));

            yield return null;
        }
    }

    private void Update()
    {
        // Check if the car is out of bounds and despawn it
        if (transform.position.z > 10f) // Change the value according to your street length
        {
            Destroy(gameObject);
        }
    }
}
