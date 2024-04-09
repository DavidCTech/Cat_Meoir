using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICursor : MonoBehaviour
{
    public bool isNpcDialog; 
    // OnEnable turns on when the script enables itself 
    private void OnEnable()
    {
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        
    }

    //OnDisable turns on when the script disables itself 
    private void OnDisable()
    {
        if (!isNpcDialog)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Invoke("KeepOnCheck", 0.1f);
        }
    }
    private void KeepOnCheck()
    {
        if (!this.gameObject.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
