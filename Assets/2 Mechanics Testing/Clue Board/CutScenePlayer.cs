using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutScenePlayer : MonoBehaviour
{
    public VideoClip[] videoClips; // Array of video clips to play
    public VideoPlayer videoPlayer; // VideoPlayer component to play the clips on
    private int currentVideoIndex = 0; // Index of the current video being played

    void Start()
    {

    }

    // Play the next video in the array
    public void PlayNextVideo()
    {
        if (currentVideoIndex < videoClips.Length)
        {
            videoPlayer.clip = videoClips[currentVideoIndex];
            videoPlayer.Play();
            currentVideoIndex++;
        }
        else
        {
            Debug.Log("All videos played.");
        }
    }
}
