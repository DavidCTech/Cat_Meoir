using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video; 

public class CutSceneClue : MonoBehaviour
{
    public bool isCutScene;
    public VideoPlayer video;
    public GameObject videoCanvas; 
    public void CutScenePlay()
    {
        if (isCutScene)
        {
            video.Play();
            videoCanvas.SetActive(true);
           
        }
    }
}
