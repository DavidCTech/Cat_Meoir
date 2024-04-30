using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBoxManager : MonoBehaviour
{
    [Header("Accessibility Toggles")]
    public Toggle tutorialBoxToggle;
    private int tutorialBoxInt;
    [HideInInspector]
    public bool isTutorialBoxesDisabled = false;

    public TutorialUI[] tutorialBoxes;

    void Awake()
    {
        tutorialBoxInt = PlayerPrefs.GetInt("TutorialBoxState");

        tutorialBoxes = FindObjectsOfType<TutorialUI>();
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("TutorialBoxState"))
        {
            if (tutorialBoxInt == 1)
            {
                tutorialBoxToggle.isOn = true;
                isTutorialBoxesDisabled = false;

                TurnOffTutorialBoxes();
            }
            else
            {
                tutorialBoxToggle.isOn = false;
                isTutorialBoxesDisabled = false;

                TurnOnTutorialBoxes();
            }
        }
    }

    public void SetTutorialBoxMode(bool isTutorialBoxesDisabled)
    {
        tutorialBoxToggle.isOn = isTutorialBoxesDisabled;
    }

    public void ApplyTutorialBoxSettings()
    {
        if (!tutorialBoxToggle.isOn)
        {
            PlayerPrefs.SetInt("TutorialBoxState", 0);
            Debug.Log("Enabling Tutorial Boxes");
            isTutorialBoxesDisabled = false;

            TurnOnTutorialBoxes();
        }
        else
        {
            PlayerPrefs.SetInt("TutorialBoxState", 1);
            Debug.Log("Disabling Tutorial Boxes");
            isTutorialBoxesDisabled = true;

            TurnOffTutorialBoxes();
        }
    }

    public void TurnOnTutorialBoxes()
    {
        for (int i = 0; i < tutorialBoxes.Length; i++)
        {
            if (!tutorialBoxes[i].gameObject.activeInHierarchy)
                tutorialBoxes[i].gameObject.transform.parent.gameObject.SetActive(true);
        }
    }

    public void TurnOffTutorialBoxes()
    {
        for (int i = 0; i < tutorialBoxes.Length; i++)
        {
            if (tutorialBoxes[i].gameObject.activeInHierarchy)
                tutorialBoxes[i].gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
