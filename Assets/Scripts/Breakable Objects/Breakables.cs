using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    public float breakHeight = 10f; // Adjust this value to set the threshold

    private float initialYPosition;
    private bool hasFallen = false;

    public GameObject[] spawnedObjects;

    private void Start()
    {
        initialYPosition = transform.position.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasFallen)
        {
            float currentYPosition = transform.position.y;
            float distanceFallen = initialYPosition - currentYPosition;

            if (distanceFallen >= breakHeight)
            {
                BreakObject();
                hasFallen = true;
            }
        }
    }

    void BreakObject()
    {
        gameObject.SetActive(false);

        if (spawnedObjects != null)
        {
            foreach (GameObject obj in spawnedObjects)
            {
                if (obj != null)
                {
                    Instantiate(obj, transform.position, transform.rotation);
                }
            }
        }
    }
}
