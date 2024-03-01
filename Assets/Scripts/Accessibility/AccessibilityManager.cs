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

    /*void Update()
    {

    }*/

    public void SetHighContrastMode(bool isUsingHighContrastMode)
    {
        highContrastToggle.isOn = isUsingHighContrastMode;

        if (!isUsingHighContrastMode)
        {
            PlayerPrefs.SetInt("HighContrastState", 0);
            Debug.Log("Turning High Contrast Mode Off");
        }
        else
        {
            PlayerPrefs.SetInt("HighContrastState", 1);
            isUsingHighContrastMode = true;
            Debug.Log("Turning High Contrast Mode On");
        }
    }

    public void SetCvHighContrastMode(bool isUsingCvHighContrastMode)
    {

    }

    public void SetJournalColorblindMode(bool isUsingJournalCb)
    {

    }

    public void SetCameraFlashMode(bool isCameraFlashDisabled)
    {

    }

    public void SetSkippingSafePuzzleMode(bool isSkippingSafePuzzles)
    {

    }

    public void SetArialDialogueMode(bool isUsingArialFont)
    {

    }

    public void SetVisualIndicatorsMode(bool isUsingVisualIndicators)
    {

    }
}
