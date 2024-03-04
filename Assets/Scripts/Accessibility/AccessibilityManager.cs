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
    public Toggle cvHighContrastToggle, journalColorblindToggle, cameraFlashToggle, skipSafePuzzleToggle,
    arialDialogueFontToggle, visualIndicatorsToggle;

    private bool isUsingHighContrastMode = false, isUsingCvHighContrastMode, isUsingJournalCb, isCameraFlashDisabled,
    isSkippingSafePuzzles, isUsingArialFont, isUsingVisualIndicators;

    private int highContrastInt, cvHighContrastInt, journalCbInt, cameraFlashInt, skipSafePuzzleInt, arialDialogueInt,
    visualIndicatorsInt;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        highContrastInt = PlayerPrefs.GetInt("HighContrastState");
        cvHighContrastInt = PlayerPrefs.GetInt("CvHighContrastState");
        journalCbInt = PlayerPrefs.GetInt("JournalCbState");
        cameraFlashInt = PlayerPrefs.GetInt("CameraFlashState");
        skipSafePuzzleInt = PlayerPrefs.GetInt("SkipSafePuzzleState");
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
            }
            else
            {
                highContrastToggle.isOn = false;
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

            if (PlayerPrefs.HasKey("SkipSafePuzzleState"))
            {
                if (skipSafePuzzleInt == 1)
                {
                    skipSafePuzzleToggle.isOn = true;
                    isSkippingSafePuzzles = true;
                }
                else
                {
                    skipSafePuzzleToggle.isOn = false;
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
                }
                else
                {
                    visualIndicatorsToggle.isOn = false;
                }
            }
        }
    }

    /*void Update()
    {

    }*/

    public void SetHighContrastMode(bool isUsingHighContrastMode)
    {
        highContrastToggle.isOn = isUsingHighContrastMode;

        if (!isUsingHighContrastMode)
        {
            PlayerPrefs.SetInt("HighContrastState", 0);
            //Debug.Log("Turning High Contrast Mode Off");
        }
        else
        {
            PlayerPrefs.SetInt("HighContrastState", 1);
            isUsingHighContrastMode = true;
            //Debug.Log("Turning High Contrast Mode On");
        }
    }

    public void SetCvHighContrastMode(bool isUsingCvHighContrastMode)
    {
        cvHighContrastToggle.isOn = isUsingCvHighContrastMode;

        if (!isUsingCvHighContrastMode)
        {
            PlayerPrefs.SetInt("CvHighContrastState", 0);
            //Debug.Log("Turning CV High Contrast Mode Off");
        }
        else
        {
            PlayerPrefs.SetInt("CvHighContrastState", 1);
            isUsingCvHighContrastMode = true;
            //Debug.Log("Turning CV High Contrast Mode On");
        }
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

    public void SetSkippingSafePuzzleMode(bool isSkippingSafePuzzles)
    {
        skipSafePuzzleToggle.isOn = isSkippingSafePuzzles;

        if (!isSkippingSafePuzzles)
        {
            PlayerPrefs.SetInt("SkipSafePuzzleState", 0);
            //Debug.Log("Turning Journal Colorblind Mode Off");
        }
        else
        {
            PlayerPrefs.SetInt("SkipSafePuzzleState", 1);
            isSkippingSafePuzzles = true;
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
        if (!visualIndicatorsToggle.isOn)
        {
            PlayerPrefs.SetInt("VisualIndicatorsState", 0);
            Debug.Log("Turning Visual Indicators Mode Off");
        }
        else
        {
            PlayerPrefs.SetInt("VisualIndicatorsState", 1);
            isUsingVisualIndicators = true;
            Debug.Log("Turning Visual Indicators Mode On");
        }
    }
}
