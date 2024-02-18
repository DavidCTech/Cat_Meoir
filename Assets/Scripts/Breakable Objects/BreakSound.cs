using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakSound : MonoBehaviour
{
    public AudioClip soundClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Play the sound clip
        if (soundClip != null)
        {
            audioSource.clip = soundClip;
            audioSource.Play();
            // Destroy the object after the sound clip finishes playing
            Destroy(gameObject, soundClip.length);
        }
        else
        {
            Debug.LogError("No sound clip assigned!");
        }
    }
}
