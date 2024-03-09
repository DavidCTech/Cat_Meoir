using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollisionHonk : MonoBehaviour
{
    public float delay;
    public string sceneName;

    void OnTriggerEnter(Collider other)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "NewHubWorld")
        {
            if (other.gameObject.tag == "Player")
            {
                PlaySound playSound = this.gameObject.GetComponent<PlaySound>();

                // Using a lambda function to delay the execution of RandomClips
                Invoke("PlayRandomClip", delay);
            }
        }
        
    }

    void PlayRandomClip()
    {
        PlaySound playSound = this.gameObject.GetComponent<PlaySound>();
        playSound.RandomClips();
    }
}
