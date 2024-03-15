using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Photo_UI_Controller : MonoBehaviour
{
    public GameObject photoFirstButton, cluesFirstButton, mapFirstButton;
    public GameObject Photo_UI;
    public GameObject Photos_Panel;
    public GameObject Clues_Panel;
    public GameObject Map_Panel;
    public PlayerController playerControls;

    private void Awake()
    {
        playerControls = new PlayerController();
    }

    public void OnEnable()
    {
        playerControls.Enable();
        playerControls.Player.OpenPhotos.performed += ctx => OnOpenPhotos();
    }

    public void OnDisable()
    {
        playerControls.Disable();
        playerControls.Player.OpenPhotos.performed -= ctx => OnOpenPhotos();
    }

    public void OnOpenPhotos()
    {
        ActivatePhotos();
    }


    public void ActivatePhotos()
    {
        Photos_Panel.SetActive(true);
        Clues_Panel.SetActive(false);
        Map_Panel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(photoFirstButton);
    }

    public void ActivateClues()
    {
        Photos_Panel.SetActive(false);
        Clues_Panel.SetActive(true);
        Map_Panel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(cluesFirstButton);
    }

    public void ActivateMap()
    {
        Photos_Panel.SetActive(false);
        Clues_Panel.SetActive(false);
        Map_Panel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mapFirstButton);
    }

}
