using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{

    public GameObject player;
    public bool faceCam; 
    private void OnEnable()
    {
        if (!faceCam)
        {
            if(player == null)
            {
                player = GameObject.FindWithTag("Player");
            }
        }
        else
        {
            if (player == null)
            {
                player = GameObject.FindWithTag("MainCamera");
            }
        }
       
    }
    void Update()
    {
        if(player != null)
        {
            this.gameObject.transform.LookAt(player.transform.position);
        }
   
        
    }

}
