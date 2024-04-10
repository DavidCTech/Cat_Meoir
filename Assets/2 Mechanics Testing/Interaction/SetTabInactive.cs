using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTabInactive : MonoBehaviour
{
    public bool isActive;
    public void SetActive()
    {
        isActive = true;
    }
    public void SetInactive()
    {
        isActive = false; 
    }
    public void Deactivate()
    {
        if (!isActive)
        {
            this.gameObject.SetActive(false);
        }
    }

}
