using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStopPause : MonoBehaviour
{
    public PauseMenu pauseMenu;
    void OnEnable()
    {
        if (pauseMenu != null)
        {
            pauseMenu.ignore = true;
        }
    }


    void OnDisable()
    {
        if (pauseMenu != null)
        {
            pauseMenu.ignore = false;
        }
    }
}
