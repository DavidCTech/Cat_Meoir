using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{

    public GameObject player; 
    void Update()
    {
        if(player != null)
        {
            this.gameObject.transform.LookAt(player.transform.position);
        }
        
    }
}
