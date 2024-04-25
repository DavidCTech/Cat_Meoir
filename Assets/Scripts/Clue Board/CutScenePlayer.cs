using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutScenePlayer : MonoBehaviour
{
    public GameObject[] enableVideo; // Array of objects to disable before playing videos

    public VideoClip[] videoClips; // Array of video clips to play
    public VideoPlayer videoPlayer; // VideoPlayer component to play the clips on
    private int currentVideoIndex = 0; // Index of the current video being played
    public bool goodEnd;

    void Start()
    {
        // Subscribe to the loopPointReached event to detect when the video has finished playing
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // Enable objects and start playing videos
    /*public void TruePlayVideos()
    {
        // Enable objects
        foreach (GameObject obj in enableVideo)
        {
            obj.SetActive(true);
        }

        // Start playing the first video
        TruePlayNextVideo();
    }

    // Play the next video in the array
    void TruePlayNextVideo()
    {
        foreach (GameObject obj in enableVideo)
        {
            obj.SetActive(true);
        }

        if (currentVideoIndex < videoClips.Length)
        {
            videoPlayer.clip = videoClips[currentVideoIndex];
            videoPlayer.Play();
            currentVideoIndex++;
        }
        else
        {
            Debug.Log("All videos played.");
            SceneManager.LoadScene("FinalChase");
            return; // Return to avoid executing the code below if all videos are played
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Play the next video
        PlayNextVideo();
    }*/

    // Enable objects and start playing videos
    public void PlayVideos()
    {
        // Enable objects
        foreach (GameObject obj in enableVideo)
        {
            obj.SetActive(true);
        }

        // Start playing the first video
        PlayNextVideo();
    }

    // Play the next video in the array
    void PlayNextVideo()
    {
        foreach (GameObject obj in enableVideo)
        {
            obj.SetActive(true);
        }

        if (currentVideoIndex < videoClips.Length)
        {
            videoPlayer.clip = videoClips[currentVideoIndex];
            videoPlayer.Play();
            currentVideoIndex++;
        }
        else
        {
            if(goodEnd)
            {
                SceneManager.LoadScene("FinalChase");
            }
            else
            {
                Debug.Log("All videos played.");
                SceneManager.LoadScene("Cutscene3");
            } 
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Play the next video
        PlayNextVideo();
    }
}
