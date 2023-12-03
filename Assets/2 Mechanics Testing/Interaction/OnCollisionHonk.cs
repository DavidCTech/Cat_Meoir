using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionHonk : MonoBehaviour
{
    public float delay;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlaySound playSound = this.gameObject.GetComponent<PlaySound>();

            // Using a lambda function to delay the execution of RandomClips
            Invoke("PlayRandomClip", delay);
        }
    }

    void PlayRandomClip()
    {
        PlaySound playSound = this.gameObject.GetComponent<PlaySound>();
        playSound.RandomClips();
    }
}
