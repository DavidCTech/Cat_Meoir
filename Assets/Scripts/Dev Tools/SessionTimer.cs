using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SessionTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timer = 0f;
    private bool timerRunning = false;

    void Start()
    {
        // Ensure the timer text is initially visible.
        timerText.enabled = true;
    }

    void Update()
    {
        // Toggle the timer visibility on/off when the "J" key is pressed.
        if (Input.GetKeyDown(KeyCode.J))
        {
            ToggleTimerVisibility();
        }

        // Toggle the timer on/off when the "H" key is pressed.
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleTimer();
        }

        // Reset the timer when the "K" key is pressed.
        if (Input.GetKeyDown(KeyCode.K))
        {
            ResetTimer();
        }

        if (timerRunning)
        {
            timer += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    void ToggleTimer()
    {
        timerRunning = !timerRunning;

        if (timerRunning)
        {
            // Timer is now running, you can add any additional logic here.
        }
        else
        {
            // Timer is stopped, you can add any additional logic here.
        }
    }

    void ToggleTimerVisibility()
    {
        // Toggle the visibility of the timer text.
        timerText.enabled = !timerText.enabled;
    }

    void ResetTimer()
    {
        // Reset the timer to zero.
        timer = 0f;

        // Update the timer display immediately after resetting.
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        // Format the timer as you wish (e.g., minutes:seconds).
        timerText.text = "Timer: " + timer.ToString("F2");
    }
}