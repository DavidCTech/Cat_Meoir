using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownController : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    private int currentSelection;

    public float scrollSpeed = 0.2f; // Adjust the scroll speed as needed

    private float timeElapsed;

    private void Start()
    {
        currentSelection = dropdown.value;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        float verticalInput = Input.GetAxis("Vertical"); // Adjust for your controller's input configuration

        if (timeElapsed > scrollSpeed)
        {
            if (verticalInput > 0)
            {
                currentSelection = Mathf.Max(0, currentSelection - 1);
            }
            else if (verticalInput < 0)
            {
                currentSelection = Mathf.Min(dropdown.options.Count - 1, currentSelection + 1);
            }

            dropdown.value = currentSelection;
            timeElapsed = 0f;
        }
    }
}
