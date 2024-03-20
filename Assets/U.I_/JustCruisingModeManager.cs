using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustCruisingModeManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objectsToToggle;

    private void Awake()
    {
        // Optionally, you can initialize the list or perform other setup tasks here.
    }

    public void ToggleObjects(bool isToggled)
    {
        Debug.Log(isToggled);

        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(isToggled);
        }
    }
}
