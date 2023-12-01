using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;
    [Header("This is for a sound delay when the method for put in clip is called." )]
    public float soundDelay;
    [Header("These are for random pitch variation within the audiosource.")]
    public bool isRandom; 
    public float maxPitch;
    public float minPitch;
    private AudioClip audioClip; 
    
    public void PutInClip(AudioClip _audioClip)
    {
        audioClip = _audioClip; 
        Invoke("PlayAudio", soundDelay);

    }
    public void PlayAudio()
    {
        if (isRandom)
        {
            float randomPitch = Random.Range(minPitch, maxPitch);
            audioSource.pitch = randomPitch;
        }
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
