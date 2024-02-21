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
        var rebinds = PlayerPrefs.GetString("rebindsCam");
        if (!string.IsNullOrEmpty(rebinds))
            actions.LoadBindingOverridesFromJson(rebinds);
    }

    public void OnDisable()
    {
        var rebinds = actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebindsCam", rebinds);

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
