using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceClipRando : MonoBehaviour
{

    AudioSource soundSource;
    [SerializeField] AudioClip[] voiceClips;
    [SerializeField] float voiceDelay = 10.0f; // Adjust in the inspector

    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        StartCoroutine(SoundClips());
    }

    IEnumerator SoundClips()
    {
        while (true)
        {
            AudioClip clip = voiceClips[UnityEngine.Random.Range(0, voiceClips.Length)];
            soundSource.PlayOneShot(clip);
            yield return new WaitForSeconds(voiceDelay); // Adjust the delay time as needed
        }
    }

}
