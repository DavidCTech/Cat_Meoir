using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryDisplay : MonoBehaviour
{
    public TMP_Text memoryText;
    private bool isMemoryDisplayActive = false; // Start with the display inactive

    void Start()
    {
        // Set the initial state of the display
        memoryText.gameObject.SetActive(isMemoryDisplayActive);
    }

    void Update()
    {
        // Toggle memory display on/off with the G key
        if (Input.GetKeyDown(KeyCode.U))
        {
            isMemoryDisplayActive = !isMemoryDisplayActive;
            memoryText.gameObject.SetActive(isMemoryDisplayActive);
        }

        // Update memory display only if it's active
        if (isMemoryDisplayActive)
        {
            // Get the current memory usage in megabytes
            float memoryUsage = (float)System.GC.GetTotalMemory(false) / (1024 * 1024);

            // Update the TMP text
            memoryText.text = $"Memory Usage: {memoryUsage:F2} MB";
        }
    }
}
