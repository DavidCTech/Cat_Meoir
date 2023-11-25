using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Photo_UI_Controller : MonoBehaviour
{
    public GameObject photoFirstButton, photoSecondButton, photoThirdButton, photoFourthButton;
    public GameObject page1;
    public GameObject page2;
    public GameObject page3;
    public GameObject funPictures;

    public void ActivatePage1()
    {
        page1.SetActive(true);
        page2.SetActive(false);
        page3.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(photoFirstButton);
    }

    public void ActivatePage2()
    {
        page1.SetActive(false);
        page2.SetActive(true);
        page3.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(photoSecondButton);
    }

    public void ActivatePage3()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(photoThirdButton);
    }

    public void ActivateFunPictures()
    {
        page1.SetActive(false);
        page2.SetActive(false);
        page3.SetActive(false);
        funPictures.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(photoFourthButton);
    }
}
