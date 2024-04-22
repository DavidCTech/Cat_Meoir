using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageTurner : MonoBehaviour
{

    public GameObject pageDisable;
    public GameObject pageEnable;

    public void TurnPage()
    {
        {
            // Disable the pageDisable object
            if (pageDisable != null)
            {
                pageDisable.SetActive(false);
            }

            // Enable the pageEnable object
            if (pageEnable != null)
            {
                pageEnable.SetActive(true);
            }
        }
    }
}
