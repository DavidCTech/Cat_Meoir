using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AP_Menu : MonoBehaviour
{
    public static AP_Menu instance;

    [Header("Paused Variables")]
    public bool isPaused;
    public CanvasGroup pauseMenu;
    public GameObject renderToggle;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            PauseUnpauseGame();
        }
    }

    public void PauseUnpauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseMenu.alpha = 1;
            pauseMenu.interactable = true;
            pauseMenu.blocksRaycasts = true;
            AP_GameManager.instance.UnlockCursor();

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(renderToggle);
        }
        else if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseMenu.alpha = 0;
            pauseMenu.interactable = false;
            pauseMenu.blocksRaycasts = false;
            AP_GameManager.instance.LockCursor();

            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
