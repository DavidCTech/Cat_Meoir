using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour, ISelectHandler
{
    public RenderPipelineAsset[] qualityLevels;
    public TMP_Dropdown dropdown;
    private ScrollRect scrollRect;
    private float scrollPosition = 1;

    public AudioMixer audioMixer;

    public GameObject mainMenuPanel;

    public GameObject optionsPanel;

    public GameObject creditsPanel;

    private Resolution[] resolutions;

    private List<Resolution> filteredResolutions;

   [SerializeField] private TMP_Dropdown resolutionDropdown;

    private int currentResolutionIndex = 0;
    private float currentRefreshRate;

    public Toggle vSyncToggle;

    public GameObject optionsFirstButton, optionsClosedButton, creditsFirstButton;

    void Start()
    {
        dropdown.value = QualitySettings.GetQualityLevel();

        filteredResolutions = new List<Resolution>();

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

       currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();

        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRate + "Hz";
            options.Add(resolutionOption);

            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.value = currentResolutionIndex; 
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
    }


    public void DeactivateMenu()
    {
        mainMenuPanel.SetActive(false);
    }

    public void ActivateOptionsMenu()
    {
        optionsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void DeactivateOptionsMenu()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        creditsPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }

    public void ActivateCreditsPanel()
    {
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsFirstButton);
    }

    public void ActivateMenu()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false); 
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (scrollRect)
            scrollRect.verticalScrollbar.value = scrollPosition;
    }


    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }


    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("MyExposedParam", volume);
        Debug.Log(volume);
    }

    public void ChangeLevel (int value)
    {
        QualitySettings.SetQualityLevel(value);
        QualitySettings.renderPipeline = qualityLevels[value];
    }


    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }


    public void ApplyVSync()
    {
        if (vSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }


    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();

    }
}
