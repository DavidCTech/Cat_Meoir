using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBoySpawner : MonoBehaviour
{
    public GameObject goodBoy;
    public float timeToEnable = 3f; // Time in seconds

    private bool isEnabled = false;

    public AudioClip soundClip;
    private AudioSource audioSource;


    void Start()
    {
        StartCoroutine(EnableObjectAfterDelay());
        audioSource = GetComponent<AudioSource>();

    }

    IEnumerator EnableObjectAfterDelay()
    {
        yield return new WaitForSeconds(timeToEnable);
        EnableObject();
        PlaySoundAndDestroy();
    }

    void EnableObject()
    {
        goodBoy.SetActive(true);
        isEnabled = true;
    }

    void PlaySoundAndDestroy()
    {
        if (soundClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(soundClip);
            Destroy(gameObject, soundClip.length);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
