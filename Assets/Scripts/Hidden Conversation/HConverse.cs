using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HConverse : MonoBehaviour
{
    public GameObject Player;
    private PlayerInteractionCheck playerInteractionCheck;

    public GameObject ClueAsset;

    public bool near;
    public float nearDistance = 5f;

    private AudioSource audioSource;
    public List<AudioClip> audioClips = new List<AudioClip>();
    public List<GameObject> uiElements = new List<GameObject>();
    private int currentIndex = 0;
    private bool isPlaying = false;


    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerInteractionCheck = Player.GetComponent<PlayerInteractionCheck>();
        audioSource = GetComponent<AudioSource>();

    }

    public void Update()
    {
        CheckNearPlayer();

        if (playerInteractionCheck.isHiding && near && !isPlaying)
        {
            isPlaying = true;
            ClueAsset.SetActive(true);
            StartCoroutine(PlayAudioClips());
        }
        else if (!playerInteractionCheck.isHiding)
        {
            isPlaying = false;
            ClueAsset.SetActive(false);
            currentIndex = 0; // Reset index if player moves away
            // Hide all UI elements when not near
            foreach (var uiElement in uiElements)
            {
                uiElement.SetActive(false);
            }
        }
    }

    public void CheckNearPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
        if (distanceToPlayer <= nearDistance)
        {
            near = true;
        }
        else
        {
            // Set the 'Near' bool to false if the AI is not near the player
            near = false;
        }
    }

    IEnumerator PlayAudioClips()
    {
        while (currentIndex < audioClips.Count)
        {
            AudioClip clip = audioClips[currentIndex];
            GameObject uiElement = uiElements[currentIndex];

            if (!ClueAsset.activeSelf)
            {
                // If it's not active, break out of the loop
                break;
            }

            // Assign the audio clip to the AudioSource component and play it
            audioSource.clip = clip;
            audioSource.Play();

            // Show UI element associated with the currently playing audio clip
            uiElement.SetActive(true);

            currentIndex++;

            // Wait for the clip to finish playing
            yield return new WaitForSeconds(clip.length);
        }

        // Reset index to play from the start
        currentIndex = 0;
    }
}