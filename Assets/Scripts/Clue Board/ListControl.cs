using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListControl : MonoBehaviour
{
    public GameObject enabledPage;
    public GameObject[] disabledPages;

    public void SuspectSelect()
    {
        // Enable the enabledPage GameObject
        enabledPage.SetActive(true);

        // Disable all GameObjects in disabledPages array
        foreach (GameObject page in disabledPages)
        {
            if (page != null) // Ensure page is not null
            {
                page.SetActive(false);
            }
        }
    }
}
