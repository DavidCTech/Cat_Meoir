using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip clickSound; 
    private Button button;

    void Start()
    {
      
        button = GetComponent<Button>();

       
        button.onClick.AddListener(PlayClickSound);
    }

    void PlayClickSound()
    {
        
        if (clickSound != null)
        {
           
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        }
        else
        {
            Debug.LogWarning("No sound effect assigned to the button.");
        }
    }
}
