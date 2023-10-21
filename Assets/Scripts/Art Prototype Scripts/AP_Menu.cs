using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AP_Menu : MonoBehaviour
{
    public static AP_Menu instance;

    [Header("Paused Variables")]
    public bool isPaused;
    public CanvasGroup pauseMenu;
    public string mainMenuScene;
    public Image reticleImage;
    public bool showReticle;
    public TMP_Text reticleText;

    [Header("Shader and Button Variables")]
    public GameObject resumeButton;


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

    public void ShowReticle()
    {
        showReticle = !showReticle;

        if (showReticle)
        {
            reticleImage.gameObject.SetActive(true);
            reticleText.text = "Reticle On";
        }
        if (!showReticle)
        {
            reticleImage.gameObject.SetActive(false);
            reticleText.text = "Reticle Off";
        }
    }

    public void QuitToMenu()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;

        SceneManager.LoadScene(mainMenuScene);
    }
}
