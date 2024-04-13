using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageCursor : MonoBehaviour
{

    
    //this script goes on the game manager, all it does it lock cursor on awake 
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


}
