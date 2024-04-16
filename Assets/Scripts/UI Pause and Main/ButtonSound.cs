using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip clickSound; 
    private Button button;
    private Toggle toggle;

    void Start()
    {
        // Try to get the button component attached to this GameObject
        button = GetComponent<Button>();

        // Try to get the toggle component attached to this GameObject
        toggle = GetComponent<Toggle>();

        // Add listeners for when the button is clicked or toggle is toggled
        if (button != null)
        {
            button.onClick.AddListener(PlayClickSound);
        }

        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(PlayClickSound);
        }
    }

    void PlayClickSound(bool value)
    {
        // Play sound for toggle
        if (toggle != null)
        {
            PlaySound();
        }
    }

    void PlayClickSound()
    {
        // Play sound for button click
        if (button != null)
        {
            PlaySound();
        }
    }

    void PlaySound()
    {
        // Check if a sound effect is assigned
        if (clickSound != null)
        {
            // Play the sound effect
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        }
        else
        {
            Debug.LogWarning("No sound effect assigned to the button or toggle.");
        }
    }
}
