using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AP_ToggleScript : MonoBehaviour
{
    [Header("UI Image Toggles")]
    public Image PpImg;
    public Image CsImg;
    public Sprite PpToggleOn, PpToggleOff, CsToggleOn, CsToggleOff;
    public bool isPPon = true, isCSon = true;
    public GameObject PPfx;

    void Update()
    {
        if (Input.GetButtonDown("Shader Toggle"))
        {
            Debug.Log("Shader");

            TurnOffShader();
        }

        if (Input.GetButtonDown("PP Toggle"))
        {
            Debug.Log("PP");

            TurnOffPP();
        }
    }

    void TurnOffShader()
    {
        isCSon = !isCSon;

        if (isCSon)
            CsImg.sprite = CsToggleOn;
        else
            CsImg.sprite = CsToggleOff;
    }

    void TurnOffPP()
    {
        isPPon = !isPPon;

        if (isPPon)
        {
            PPfx.gameObject.SetActive(true);
            PpImg.sprite = PpToggleOn;
        }
        else
        {
            PPfx.gameObject.SetActive(false);
            PpImg.sprite = PpToggleOff;
        }
    }
}
