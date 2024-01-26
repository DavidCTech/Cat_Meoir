using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeveloperTools : MonoBehaviour
{
    private static DeveloperTools instance;
    public GameObject fastTravel;  // Change from Canvas to GameObject
    public Slider timeScaleSlider;

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

    void Start()
    {
        if (timeScaleSlider != null)
        {
            // Set the initial value of the slider to the current time scale
            timeScaleSlider.value = Time.timeScale;

            // Add a listener to respond to changes in the slider's value
            timeScaleSlider.onValueChanged.AddListener(UpdateTimeScale);
        }
        else
        {
            Debug.LogError("Slider not assigned!");
        }
    }

    void UpdateTimeScale(float value)
    {
        // Update the game's time scale based on the slider's value
        Time.timeScale = value;
    }

    private void ReloadScene()
    {
        // Get the current scene index and reload it
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadSceneByName(string sceneName)
    {
        // Load the scene by name
        SceneManager.LoadScene(sceneName);

        // Disable the GameObject instead of setting Canvas.enabled
        fastTravel.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ReloadScene();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            // Toggle the active state of the GameObject
            fastTravel.SetActive(!fastTravel.activeSelf);

            if (SceneManager.GetActiveScene().name != "Menu")
            {
                if (fastTravel.activeSelf)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }

            /*if (Input.GetKeyDown(KeyCode.U))
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
            }*/
        }
    }
}