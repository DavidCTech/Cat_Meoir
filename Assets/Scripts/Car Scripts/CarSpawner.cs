using System.Collections;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public Transform[] waypoints;
    public float spawnInterval = 5f;

    private void Start()
    {
        InvokeRepeating("SpawnCar", 0f, spawnInterval);
    }

    private void SpawnCar()
    {
        GameObject selectedCarPrefab = SelectACarPrefab();
        GameObject spawnedCar = Instantiate(selectedCarPrefab, transform.position, transform.rotation);

        // Attach the CarMovement script to the spawned car
        CarMovement carMovement = spawnedCar.AddComponent<CarMovement>();
        carMovement.waypoints = waypoints;
    }

    private GameObject SelectACarPrefab()
    {
        var randomIndex = Random.Range(0, carPrefabs.Length);
        return carPrefabs[randomIndex];
    }
}