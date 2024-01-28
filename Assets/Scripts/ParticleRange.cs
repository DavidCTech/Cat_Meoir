using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRange : MonoBehaviour
{
    public ParticleSystem particleSystem; // Reference to your Particle System
    public float detectionRange = 5f;     // Range to trigger the particle effect
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the particle system is not playing at the start
        if (particleSystem.isPlaying)
        {
            particleSystem.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is within the detection range
        if (Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            // If the particle system is not playing, start it
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
                Debug.Log("Particle System started.");
            }
        }
        else
        {
            // If the player is outside the range, stop the particle system
            if (particleSystem.isPlaying)
            {
                particleSystem.Stop();
                Debug.Log("Particle System stopped.");
            }
        }
    }
}
