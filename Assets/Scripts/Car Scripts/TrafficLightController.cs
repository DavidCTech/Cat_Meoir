using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public GameObject[] stopBoxes; // Invisible boxes representing the stop zones
    public float greenLightDuration = 10f; // Duration of the green light
    public float yellowLightDuration = 2f; // Duration of the yellow light
    public float redLightDuration = 5f; // Duration of the red light

    private bool isGreen = false;

    private void Start()
    {
        // Start the traffic light cycle
        StartCoroutine(TrafficLightCycle());
    }

    private IEnumerator TrafficLightCycle()
    {
        while (true)
        {
            // Set the light to green
            SetLights(true);
            yield return new WaitForSeconds(greenLightDuration);

            // Set the light to yellow
            SetLights(false);
            yield return new WaitForSeconds(yellowLightDuration);

            // Set the light to red
            SetLights(false);
            yield return new WaitForSeconds(redLightDuration);
        }
    }

    private void SetLights(bool isGreen)
    {
        this.isGreen = isGreen;
        if (isGreen)
        {
            // Turn on green lights, allow cars to move
            foreach (var box in stopBoxes)
            {
                box.SetActive(false);
            }
        }
        else
        {
            // Turn on red lights, stop cars
            foreach (var box in stopBoxes)
            {
                box.SetActive(true);
            }
        }
    }

    public bool IsGreen()
    {
        return isGreen;
    }
}
