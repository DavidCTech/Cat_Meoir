using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class AP_MainMenu : MonoBehaviour
{
    public CanvasGroup startMenu;
    public GameObject artPrototypeButton;
    public string artPrototypeScene;

    void Start()
    {

    }

    void Update()
    {

    }

    public void StartButton()
    {
        Debug.Log("Working");
        startMenu.alpha = 0;
        startMenu.interactable = false;
        startMenu.blocksRaycasts = false;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(artPrototypeButton);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(artPrototypeScene);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quiting");
    }
}
