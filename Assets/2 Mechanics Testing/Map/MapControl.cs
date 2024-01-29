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
            TimeScale();
        }
    }

    void TimeScale()
    {
        // Check the current timescale and adjust it accordingly
        if (Time.timeScale == 1.0f)
        {
            // If timescale is normal (1.0), slow down the game
            Time.timeScale = 0.0f; // You can adjust this value as needed
        }
        else
        {
            // If timescale is not normal, set it back to normal
            Time.timeScale = 1.0f;
        }
    }
}
