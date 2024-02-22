using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetBindings : MonoBehaviour
{
    public InputActionAsset playerInput;
    public InputActionAsset cameraInput;
    [SerializeField] private string playerControlScheme, cameraControlScheme;

    public void ResetSchemeBindings()
    {
        foreach (InputActionMap map in playerInput.actionMaps)
        {
            foreach (InputAction action in map.actions)
            {
                action.RemoveBindingOverride(InputBinding.MaskByGroup(playerControlScheme));
            }
        }

        foreach (InputActionMap map in cameraInput.actionMaps)
        {
            foreach (InputAction action in map.actions)
            {
                action.RemoveBindingOverride(InputBinding.MaskByGroup(cameraControlScheme));
            }
        }
    }
}
