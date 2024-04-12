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
    public float distanceToTeleport;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (hasCollided == false)
            {
                if (Vector3.Distance(thePlayer.transform.position, theKiller.transform.position) > distanceToTeleport)
                {
                    this.gameObject.SetActive(false);
                    theKiller.position = teleportPoint.position;
                    Debug.Log("Teleporting Killer");
                    hasCollided = true;
                }
                else
                {
                    this.gameObject.SetActive(false);
                    Debug.Log("Turning off Collider");
                    hasCollided = true;
                }
            }
        }
    }
}
