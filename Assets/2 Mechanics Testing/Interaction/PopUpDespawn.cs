using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpDespawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public ClimbRaycast climbRaycast;
    public Vector3 highestPoint;
    public float heightDistance; 
    void OnEnable()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            climbRaycast = player.GetComponent<ClimbRaycast>();
        }
    }
    //set the object this Ui is on 
    public void SetObject(Vector3 _highestPoint)
    {
        highestPoint = _highestPoint; 
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if(highestPoint != null)
            {
                // issue with height and player data look later 
                /*
                heightDistance = highestPoint.y - player.transform.position.y;
                if (heightDistance <= climbRaycast.maxDistance && heightDistance >= -0.2)
                {
                    this.gameObject.SetActive(true);
                }
                else
                {
                    this.gameObject.SetActive(false);
                }
                */ 
            }
           


          
        }
    }
}
