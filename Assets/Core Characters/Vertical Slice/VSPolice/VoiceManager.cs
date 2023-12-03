using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    AudioSource soundSource;
    [SerializeField] AudioClip[] voiceClips;
    [SerializeField] AudioClip[] voiceAware;
    [SerializeField] AudioClip[] voiceLost;
    [SerializeField] float voiceDelay = 10.0f; // Adjust in the inspector

    public GameObject policeVS;
    public Human_AI voiceHuman;

    private bool isPlayingAudio = false;

    void Start()
    {
        voiceHuman = policeVS.GetComponent<Human_AI>();
        soundSource = GetComponent<AudioSource>();
        StartCoroutine(SoundClips());
    }

    private void Update()
    {
        if (voiceHuman._canSeePlayer == true && !isPlayingAudio)
        {
            StartCoroutine(SoundAware());
        }
        else if (voiceHuman._canSeePlayer == false && voiceHuman.hasSeenPlayer == true && !isPlayingAudio)
        {
            StartCoroutine(SoundLost());
        }
    }

    IEnumerator SoundClips()
    {
        isPlayingAudio = true;
        AudioClip clip = voiceClips[UnityEngine.Random.Range(0, voiceClips.Length)];
        soundSource.PlayOneShot(clip);
        yield return new WaitForSeconds(voiceDelay); // Adjust the delay time as needed
        isPlayingAudio = false;
    }

    IEnumerator SoundAware()
    {
        isPlayingAudio = true;
        AudioClip clip = voiceAware[UnityEngine.Random.Range(0, voiceAware.Length)];
        soundSource.PlayOneShot(clip);
        yield return new WaitForSeconds(voiceDelay); // Adjust the delay time as needed
        isPlayingAudio = false;
    }

    IEnumerator SoundLost()
    {
        isPlayingAudio = true;
        AudioClip clip = voiceLost[UnityEngine.Random.Range(0, voiceLost.Length)];
        soundSource.PlayOneShot(clip);
        yield return new WaitForSeconds(voiceDelay); // Adjust the delay time as needed
        isPlayingAudio = false;
    }
}
