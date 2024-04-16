using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfirmationPopup : MonoBehaviour
{
  
    private bool userConfirmed;

    public void OnApplyButtonClick()
    {
        userConfirmed = true;
        ClosePopup();
    }

    public void OnCancelButtonClick()
    {
        userConfirmed = false;
        ClosePopup();
    }
    
    public void ShowPopup(string message)
    {
        userConfirmed = false;

        
        gameObject.SetActive(true);
    }
    
    void ClosePopup()
    {
        
        gameObject.SetActive(false);
    }

    public bool UserConfirmed()
    {
        return userConfirmed;
    }
}
