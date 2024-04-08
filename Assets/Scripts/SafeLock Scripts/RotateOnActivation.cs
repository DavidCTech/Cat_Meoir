using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnActivation : MonoBehaviour
{
    public GameObject objectToRotate; // Reference to the object to rotate
    public Vector3 rotationAmount; // Amount of rotation to apply when activated
    public AudioClip rotationSound; // Sound to play when rotating

    private AudioSource audioSource; // Reference to the AudioSource component

    private void Start()
    {
        // Ensure object to rotate is provided
        if (objectToRotate == null)
        {
            Debug.LogError("Object to rotate is not assigned in RotateOnActivation script!");
        }
        else
        {
            // Rotate the object immediately upon start
            objectToRotate.transform.Rotate(rotationAmount);

            // Add AudioSource component if not already present
            audioSource = objectToRotate.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = objectToRotate.AddComponent<AudioSource>();
            }

            // Set the audio clip
            audioSource.clip = rotationSound;
        }
    }

    // Function to rotate the object and play the sound
    public void RotateAndPlaySound()
    {
        // Rotate the object
        objectToRotate.transform.Rotate(rotationAmount);

        // Play the sound
        if (audioSource != null && rotationSound != null)
        {
            audioSource.PlayOneShot(rotationSound);
        }
    }
}
