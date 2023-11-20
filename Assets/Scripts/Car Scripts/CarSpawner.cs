using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public float spawnInterval = 5f; // Set the time interval between spawns
    public float despawnDelay = 12f; // Set the delay before despawning a car


    private void Start()
    {
        // Start spawning cars after a delay of 0 seconds, and repeat every spawnInterval seconds
        InvokeRepeating("SpawnCar", 0f, spawnInterval);
    }
    private void SpawnCar()
    {
        GameObject selectedCarPrefab = SelectACarPrefab();
        GameObject spawnedCar = Instantiate(selectedCarPrefab, transform.position, transform.rotation);

        // Destroy the spawned car after despawnDelay seconds
        Destroy(spawnedCar, despawnDelay);
    }

    private GameObject SelectACarPrefab()
    {
        var randomIndex = Random.Range(0, carPrefabs.Length);
        return carPrefabs[randomIndex];
    }
}
