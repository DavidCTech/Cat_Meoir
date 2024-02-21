using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MySaveLoadRebind : MonoBehaviour
{
    public InputActionAsset actions;
    public InputManager inputPlayer;
    public OpenPhotos inputPlayerPhoto;

    public void OnEnable()
    {
        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
            actions.LoadBindingOverridesFromJson(rebinds);
    }

    public void OnDisable()
    {
        var rebinds = actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);

        if (inputPlayer != null)
        {
            inputPlayer.ActionsResetAndLoad();
        }

        if (inputPlayerPhoto != null)
        {
            inputPlayerPhoto.ActionsResetAndLoad();
        }

    }
}
