using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenPhotos : MonoBehaviour
{
    //bug fixed with chatgpt 
    private PlayerController playerControls;
    public GameObject photoMenu;
    private bool isOn = false;
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerController();

        }

        playerControls.Player.OpenPhotos.performed += OnOpenPhoto;
        playerControls.Player.OpenPhotos.Enable();

        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
            playerControls.asset.LoadBindingOverridesFromJson(rebinds);
    }

    private void OnDisable()
    {
        playerControls.Player.OpenPhotos.performed -= OnOpenPhoto;
        playerControls.Player.OpenPhotos.Disable();
    }
    public void OnOpenPhoto(InputAction.CallbackContext context)
    {
        if (isOn)
        {

            photoMenu.SetActive(false);
            isOn = false;
        }
        else
        {
            photoMenu.SetActive(true);
            isOn = true;
        }
    }

    public void ActionsResetAndLoad()
    {
        playerControls.Player.OpenPhotos.performed -= OnOpenPhoto;
        playerControls.Player.OpenPhotos.Disable();

        playerControls = new PlayerController();

        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
            playerControls.asset.LoadBindingOverridesFromJson(rebinds);

        playerControls.Player.OpenPhotos.performed += OnOpenPhoto;
        playerControls.Player.OpenPhotos.Enable();
    }
}
