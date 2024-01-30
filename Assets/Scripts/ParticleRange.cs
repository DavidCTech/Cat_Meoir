using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRange : MonoBehaviour
{
    private ParticleSystem particleSystemInstance; // Reference to the instantiated Particle System
    public GameObject particlePrefab; // Reference to the Particle System prefab
    public float detectionRange = 5f;     // Range to trigger the particle effect
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate the particle system prefab and attach it to the same GameObject
        if (particlePrefab != null)
        {
            // Set the rotation to a 90-degree angle around the Y-axis
            Quaternion initialRotation = Quaternion.Euler(-90f, 0f, 0f);
            particleSystemInstance = Instantiate(particlePrefab, transform.position, initialRotation, transform).GetComponent<ParticleSystem>();
            // Disable the emission by default
            particleSystemInstance.Stop();
        }
        else
        {
            Debug.LogError("Particle Prefab is not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is within the detection range
        if (Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            // If the particle system is not playing, start it
            if (!particleSystemInstance.isPlaying)
            {
                particleSystemInstance.Play();
                Debug.Log("Particle System started.");
            }
        }
        else
        {
            // If the player is outside the range, stop the particle system
            if (particleSystemInstance.isPlaying)
            {
                particleSystemInstance.Stop();
                Debug.Log("Particle System stopped.");
            }
        }
    }
}
