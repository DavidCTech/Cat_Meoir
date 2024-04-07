using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneCursor : MonoBehaviour
{
    public GameObject journalMenu;
    public void On()
    {
        if (!journalMenu.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    public void Off()
    {
        if (!journalMenu.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
}
