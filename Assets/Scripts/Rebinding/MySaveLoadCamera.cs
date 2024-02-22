using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MySaveLoadCamera : MonoBehaviour
{
    public InputActionAsset actions;
    public CameraSwitch inputCameraSwitch;
    public CameraTakePicture inputCameraTakePicture;

    public void OnEnable()
    {
        var rebindsCam = PlayerPrefs.GetString("rebindsCam");
        if (!string.IsNullOrEmpty(rebindsCam))
            actions.LoadBindingOverridesFromJson(rebindsCam);
    }

    public void OnDisable()
    {
        var rebindsCam = actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebindsCam", rebindsCam);

        if (inputCameraSwitch != null)
        {
            inputCameraSwitch.ActionsResetAndLoad();
        }

        if (inputCameraTakePicture != null)
        {
            inputCameraTakePicture.ActionsResetAndLoad();
        }

    }
}
