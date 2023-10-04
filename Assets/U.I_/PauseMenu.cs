using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;
    public GameObject pauseMenuUI;
    private bool isPaused;


    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }


    // Update is called once per frame
    private void Update()
    {
        if (InputManagerMenu.instance.MenuOpenCloseInput)
        {
            if (!isPaused)
            {
                Pause();
            }

            else
            {
                Resume();
            }
        }

    }

    public void Resume()
    {
       pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    

}
