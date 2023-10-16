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

    [Header("Shader and Button Variables")]
    public GameObject resumeButton;
    public GameObject ppEffects;
    public bool isShaderOn;

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

        if (Input.GetButtonDown("Shader Toggle") && !isPaused)
        {
            Debug.Log("Shader");

            TurnOffShader();
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
            EventSystem.current.SetSelectedGameObject(resumeButton);
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

    public void TurnOffShader()
    {
        isShaderOn = !isShaderOn;

        if (isShaderOn)
        {
            ppEffects.gameObject.SetActive(true);
        }
        else if (!isShaderOn)
        {
            ppEffects.gameObject.SetActive(false);
        }
    }
}
