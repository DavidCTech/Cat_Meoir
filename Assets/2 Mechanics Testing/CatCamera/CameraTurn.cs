using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTurn : MonoBehaviour
{
    //this script goes on the camera
    public bool canTurn = false; 
  

 
    void Update()
    {
        if (canTurn)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //code is copy pasted from internet for easy use
                Vector3 mouse = Input.mousePosition;
                Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x,mouse.y,this.gameObject.transform.position.y));
                Vector3 forward = mouseWorld - this.gameObject.transform.position;
                this.gameObject.transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
            }
        }
        
    }
}
