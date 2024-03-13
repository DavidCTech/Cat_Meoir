using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class AccessibilityManager : MonoBehaviour
{
    public static AccessibilityManager instance;

    [Header("Accessibility Toggles")]
    public Toggle highContrastToggle;
    public Toggle cvHighContrastToggle, journalColorblindToggle, cameraFlashToggle,
    arialDialogueFontToggle, visualIndicatorsToggle;

    private bool isUsingHighContrastMode = false, isUsingCvHighContrastMode, isUsingJournalCb, isCameraFlashDisabled,
    isUsingArialFont, isUsingVisualIndicators;

    private int highContrastInt, cvHighContrastInt, journalCbInt, cameraFlashInt, arialDialogueInt,
    visualIndicatorsInt;

    void Awake()
    {
        //Make script an instance
        if (instance == null)
        {
            instance = this;
        }

        highContrastInt = PlayerPrefs.GetInt("HighContrastState");
        cvHighContrastInt = PlayerPrefs.GetInt("CvHighContrastState");
        journalCbInt = PlayerPrefs.GetInt("JournalCbState");
        cameraFlashInt = PlayerPrefs.GetInt("CameraFlashState");
        arialDialogueInt = PlayerPrefs.GetInt("ArialDialogueState");
        visualIndicatorsInt = PlayerPrefs.GetInt("VisualIndicatorsState");
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("HighContrastState"))
        {
            if (highContrastInt == 1)
            {
                highContrastToggle.isOn = true;
                isUsingHighContrastMode = true;

                if (FindObjectOfType<HighContrastManager>() != null)
                {
                    HighContrastManager.instance.SwapMaterials();
                }
            }
            else
            {
                highContrastToggle.isOn = false;

                if (FindObjectOfType<HighContrastManager>() != null)
                {
                    HighContrastManager.instance.SwapMaterials();
                }
            }
        }

        if (PlayerPrefs.HasKey("CvHighContrastState"))
        {
            if (cvHighContrastInt == 1)
            {
                cvHighContrastToggle.isOn = true;
                isUsingCvHighContrastMode = true;
            }
            else
            {
                cvHighContrastToggle.isOn = false;
            }
        }

        if (PlayerPrefs.HasKey("JournalCbState"))
        {
            if (journalCbInt == 1)
            {
                journalColorblindToggle.isOn = true;
                isUsingJournalCb = true;
            }
            else
            {
                journalColorblindToggle.isOn = false;
            }
        }

        if (PlayerPrefs.HasKey("CameraFlashState"))
        {
            if (cameraFlashInt == 1)
            {
                cameraFlashToggle.isOn = true;
                isCameraFlashDisabled = true;
            }
            else
            {
                cameraFlashToggle.isOn = false;
            }
        }

        if (PlayerPrefs.HasKey("ArialDialogueState"))
        {
            if (arialDialogueInt == 1)
            {
                arialDialogueFontToggle.isOn = true;
                isUsingArialFont = true;
            }
            else
            {
                arialDialogueFontToggle.isOn = false;
            }
        }

        if (PlayerPrefs.HasKey("VisualIndicatorsState"))
        {
            if (visualIndicatorsInt == 1)
            {
                visualIndicatorsToggle.isOn = true;
                isUsingVisualIndicators = true;
                VisualIndicators.instance.visualIndicatorsBG.SetActive(false);
            }
            else
            {
                visualIndicatorsToggle.isOn = false;
                VisualIndicators.instance.visualIndicatorsBG.SetActive(false);
            }
        }
    }

    public void SetHighContrastMode(bool isUsingHighContrastMode)
    {
        highContrastToggle.isOn = isUsingHighContrastMode;
    }

    public void SetCvHighContrastMode(bool isUsingCvHighContrastMode)
    {
        cvHighContrastToggle.isOn = isUsingCvHighContrastMode;
    }

    public void SetJournalColorblindMode(bool isUsingJournalCb)
    {
        journalColorblindToggle.isOn = isUsingJournalCb;

        if (!isUsingJournalCb)
        {
            PlayerPrefs.SetInt("JournalCbState", 0);
            //Debug.Log("Turning Journal Colorblind Mode Off");
        }
        else
        {
            PlayerPrefs.SetInt("JournalCbState", 1);
            isUsingJournalCb = true;
            //Debug.Log("Turning Journal Colorblind Mode On");
        }
    }

    public void SetCameraFlashMode(bool isCameraFlashDisabled)
    {
        cameraFlashToggle.isOn = isCameraFlashDisabled;

        if (!isCameraFlashDisabled)
        {
            PlayerPrefs.SetInt("CameraFlashState", 0);
            //Debug.Log("Turning Journal Colorblind Mode Off");
        }
        else
        {
            PlayerPrefs.SetInt("CameraFlashState", 1);
            isCameraFlashDisabled = true;
            //Debug.Log("Turning Journal Colorblind Mode On");
        }
    }

    public void SetArialDialogueMode(bool isUsingArialFont)
    {
        arialDialogueFontToggle.isOn = isUsingArialFont;

        if (!isUsingArialFont)
        {
            PlayerPrefs.SetInt("ArialDialogueState", 0);
            //Debug.Log("Turning Journal Colorblind Mode Off");
        }
        else
        {
            PlayerPrefs.SetInt("ArialDialogueState", 1);
            isUsingArialFont = true;
            //Debug.Log("Turning Journal Colorblind Mode On");
        }
    }

    public void SetVisualIndicatorsMode(bool isUsingVisualIndicators)
    {
        visualIndicatorsToggle.isOn = isUsingVisualIndicators;
    }

    public void ApplySettings()
    {
        if (!highContrastToggle.isOn)
        {
            PlayerPrefs.SetInt("HighContrastState", 0);

            if (FindObjectOfType<HighContrastManager>() != null)
            {
                HighContrastManager.instance.SwapMaterials();
            }
            //Debug.Log("Turning High Contrast Mode Off");
        }
        else
        {
            PlayerPrefs.SetInt("HighContrastState", 1);
            isUsingHighContrastMode = true;

            if (FindObjectOfType<HighContrastManager>() != null)
            {
                HighContrastManager.instance.SwapMaterials();
            }
            //Debug.Log("Turning High Contrast Mode On");
        }

        if (!cvHighContrastToggle.isOn)
        {
            PlayerPrefs.SetInt("CvHighContrastState", 0);
            if (FindObjectOfType<HighContrastManager>() != null)
            {
                if (HighContrastManager.instance.isUsingCatVision)
                {
                    HighContrastManager.instance.SwapMaterialsInCatVision();
                }
            }
            //Debug.Log("Turning Journal Colorblind Mode Off");
        }
        else
        {
            PlayerPrefs.SetInt("CvHighContrastState", 1);
            if (FindObjectOfType<HighContrastManager>() != null)
            {
                if (HighContrastManager.instance.isUsingCatVision)
                {
                    HighContrastManager.instance.SwapMaterialsInCatVision();
                }
            }
            isUsingHighContrastMode = true;
            //Debug.Log("Turning Journal Colorblind Mode On");
        }

        if (!visualIndicatorsToggle.isOn)
        {
            PlayerPrefs.SetInt("VisualIndicatorsState", 0);
            VisualIndicators.instance.visualIndicatorsBG.SetActive(false);
            Debug.Log("Turning Visual Indicators Mode Off");
        }
        else
        {
            PlayerPrefs.SetInt("VisualIndicatorsState", 1);
            isUsingVisualIndicators = true;

            if (FindAnyObjectByType<VisualIndicators>() != null)
            {
                if (!VisualIndicators.instance.visualIndicatorsBG.activeInHierarchy)
                {
                    VisualIndicators.instance.visualIndicatorsBG.SetActive(true);
                    VisualIndicators.instance.visualIndicatorsText.text = "Visual Indicators On";
                }
            }

            Debug.Log("Turning Visual Indicators Mode On");
        }
    }
}
