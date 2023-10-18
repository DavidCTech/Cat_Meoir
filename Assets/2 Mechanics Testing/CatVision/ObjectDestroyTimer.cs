using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This goes on any object you want destroyed after x amount of time 
public class ObjectDestroyTimer : MonoBehaviour
{
    public float destroyTime; 
    private void Start()
    {
        Invoke("Destroy", destroyTime);
    }

    private void Destroy()
    {
        Destroy(this.gameObject); 
    }
}
