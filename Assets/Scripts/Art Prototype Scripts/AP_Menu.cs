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
    // public Shader shader = Shader.Find("CelShading");


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

        Shader targetShader = Shader.Find("CelShading");
        Renderer[] renderers = FindObjectsOfType<Renderer>();

        // if (isShaderOn)
        // {

        //     ppEffects.gameObject.SetActive(true);
        // }
        // else if (!isShaderOn)
        // {

        //     ppEffects.gameObject.SetActive(false);
        // }

        // Camera[] cameras = FindObjectsOfType<Camera>();

        // foreach (Camera camera in cameras)
        // {
        //     camera.enabled = isShaderOn;
        // }
        foreach (Renderer renderer in renderers)
        {
            if (renderer.material.shader == targetShader)
            {
                // This renderer uses the shader you want to turn off
                // You can set a different material or change shader properties here
                // For example, you can replace it with a standard material to turn off the effect
                renderer.material = new Material(Shader.Find("Standard"));
            }

        }
    }
}
