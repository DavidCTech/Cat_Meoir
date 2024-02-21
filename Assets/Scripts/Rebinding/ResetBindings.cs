using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetBindings : MonoBehaviour
{
    public InputActionAsset _inputActionAsset;
    [SerializeField] private string targetControlScheme;

    public void ResetSchemeBindings()
    {
        foreach (InputActionMap map in _inputActionAsset.actionMaps)
        {
            foreach (InputAction action in map.actions)
            {
                action.RemoveBindingOverride(InputBinding.MaskByGroup(targetControlScheme));
            }
        }
    }
}
