using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestingScript : MonoBehaviour
{

    private static TestingScript instance;

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
        if (Input.GetKeyDown(KeyCode.I))
        {
            ReloadScene();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadSceneByName("Menu");
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            LoadSceneByName("VerticalSliceAlleyway");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            LoadSceneByName("VerticalSlice");
        }

        if (Input.GetKeyDown(KeyCode.L))
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
