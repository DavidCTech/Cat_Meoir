using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AP_ToggleScript : MonoBehaviour
{
    public Image PPimg, CSimg;
    public Sprite PPtoggleOn, PPtoggleOff, CStoggleOn, CStoggleOff;

    public bool isPPon = true, isCSon = true;

    public GameObject PPfx;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
            CSimg.sprite = CStoggleOn;
        else
            CSimg.sprite = CStoggleOff;
    }

    void TurnOffPP()
    {
        isPPon = !isPPon;

        if (isPPon)
        {
            PPfx.gameObject.SetActive(true);
            PPimg.sprite = PPtoggleOn;
        }
        else
        {
            PPfx.gameObject.SetActive(false);
            PPimg.sprite = PPtoggleOff;
        }
    }
}
