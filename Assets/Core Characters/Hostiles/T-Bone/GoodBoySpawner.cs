using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBoySpawner : MonoBehaviour
{
    public GameObject goodBoy;
    public float timeToEnable = 3f; // Time in seconds

    private bool isEnabled = false;

    void Start()
    {
        StartCoroutine(EnableObjectAfterDelay());
    }

    IEnumerator EnableObjectAfterDelay()
    {
        yield return new WaitForSeconds(timeToEnable);
        EnableObject();
        Destroy(gameObject); // Destroy the script's GameObject
    }

    void EnableObject()
    {
        goodBoy.SetActive(true);
        isEnabled = true;
    }
}
