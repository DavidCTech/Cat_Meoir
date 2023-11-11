using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Delete

public class MapControl : MonoBehaviour
{
    public GameObject playerMap;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Load the specified scene
            SceneManager.LoadScene("MapScene2");
        }
    }

    void OnMapOpenClose()
    {
        if (playerMap != null)
        {
            // Toggle the map UI's visibility
            playerMap.SetActive(!playerMap.activeSelf);
        }
    }
}
