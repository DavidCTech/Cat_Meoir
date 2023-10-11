using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICursor : MonoBehaviour
{
    // OnEnable turns on when the script enables itself 
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //OnDisable turns on when the script disables itself 
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
