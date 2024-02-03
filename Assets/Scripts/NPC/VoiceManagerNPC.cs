using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManagerNPC : MonoBehaviour
{
    AudioSource soundSource;
    [SerializeField] AudioClip[] voiceClips;
    [SerializeField] AudioClip[] voiceAware;
    [SerializeField] AudioClip[] voiceLost;
    [SerializeField] float voiceDelay = 10.0f; // Adjust in the inspector

    public bool inRange;
    private bool isPlayingAudio = false;

    public GameObject Player;
    private PlayerInteractionCheck playerInteractionCheck;

    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public float proximityRadius;
    [Range(0, 360)] public float proximityAngle;

    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        StartCoroutine(SoundClips());
        // Find the player GameObject by tag
        Player = GameObject.FindGameObjectWithTag("Player");
        playerInteractionCheck = Player.GetComponent<PlayerInteractionCheck>();
    }

    void Update()
    {
        ProximityCheck();
        if (inRange && !isPlayingAudio)
        {
            StartCoroutine(SoundAware());
        }
        else if (playerInteractionCheck.isHiding && !isPlayingAudio)
        {
            StartCoroutine(SoundLost());
        }
        else if (!isPlayingAudio)  // Only start a new coroutine if no coroutine is already playing
        {
            StartCoroutine(SoundClips());
        }
    }

    private void ProximityCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, proximityRadius, targetMask);

        inRange = rangeChecks.Length > 0;

        if (inRange)
        {
            Transform target = rangeChecks[0].transform;
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // Obstruction check
            if (!Physics.Raycast(transform.position, (target.position - transform.position).normalized, distanceToTarget, obstructionMask))
            {
                // Player is in range and not obstructed
                inRange = true;
            }
            else
            {
                // Player is obstructed
                inRange = false;
            }
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
