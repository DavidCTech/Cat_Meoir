using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillerTeleporter : MonoBehaviour
{
    private bool hasCollided = false;
    public Transform thePlayer;
    public Transform theKiller;
    public Transform teleportPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hasCollided == false)
            {
                theKiller.position = teleportPoint.position;
                Debug.Log("Teleporting Killer");
                hasCollided = true;
            }
        }
    }
}
