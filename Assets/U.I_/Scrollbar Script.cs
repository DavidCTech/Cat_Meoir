using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarScript : MonoBehaviour
{
    public Scrollbar scrollbar;

    private void Update()
    {
        // Use controller input to scroll the scrollbar
        float verticalInput = Input.GetAxis("Vertical");
        scrollbar.value = Mathf.Clamp01(scrollbar.value + verticalInput * Time.deltaTime);
    }
}
