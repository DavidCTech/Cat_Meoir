using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeveloperTools : MonoBehaviour
{

    private static DeveloperTools instance;

    private void Awake()
    {
        // Ensure there's only one instance of this script
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ReloadScene();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            LoadSceneByName("Menu");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            LoadSceneByName("Apartment");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            LoadSceneByName("Bookstore");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadSceneByName("ShoeStore");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            LoadSceneByName("VerticalSliceAlleyway");
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadSceneByName("VerticalSlice");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadSceneByName("VerticalSliceRun");
        }
    }

    private void ReloadScene()
    {
        // Get the current scene index and reload it
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadSceneByName(string sceneName)
    {
        // Load the scene by name
        SceneManager.LoadScene(sceneName);
    }
}
