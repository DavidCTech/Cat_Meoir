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
using UnityEngine.Events;

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

    Resolution[] resolutions;

    private List<Resolution> filteredResolutions;

   [SerializeField] private TMP_Dropdown resolutionDropdown;

    private int currentResolutionIndex = 0;
    private float currentRefreshRate;

    public Slider volumeSlider;
    public Toggle fullScreenToggle;
    private int screenInt;
    public bool isFullScreen = false;

    public PlayerController playerControls;

    public Toggle vSyncToggle;

    public GameObject optionsFirstButton, optionsClosedButton, creditsFirstButton;

    const string prefName = "optionsvalue";
    const string resName = "resolutionoption";

    private void Awake()
    {
        screenInt = PlayerPrefs.GetInt("togglestate");
        if (screenInt == 1)
        {
            isFullScreen = true;
            fullScreenToggle.isOn = true;
        }
        else
        {
            fullScreenToggle.isOn = false;
        }

        resolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(resName, resolutionDropdown.value);
            PlayerPrefs.Save();
        }));
        dropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(prefName, dropdown.value);
        }));

        playerControls = new PlayerController();
    }

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("MVolume");
        audioMixer.SetFloat("MyExposedParam", PlayerPrefs.GetFloat("MVolume"));

        dropdown.value = PlayerPrefs.GetInt(prefName, 3);

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        currentRefreshRate = Screen.currentResolution.refreshRate;

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionOption = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            options.Add(resolutionOption);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height &&
                resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        resolutionDropdown.value = PlayerPrefs.GetInt(resName, currentResolutionIndex);

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

    }


    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    public void SetVolume (float sliderValue)
    {
        PlayerPrefs.SetFloat("MVolume", sliderValue);
        audioMixer.SetFloat("MyExposedParam", PlayerPrefs.GetFloat("MVolume"));
        audioMixer.SetFloat("MyExposedParam", Mathf.Log10(sliderValue) * 20);
    }

    public void ChangeLevel (int value)
    {
        QualitySettings.SetQualityLevel(value);
        QualitySettings.renderPipeline = qualityLevels[value];
    }


    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        if (isFullscreen == false)
        {
            PlayerPrefs.SetInt("togglestate", 0);
        }
        else
        {
            isFullscreen = true;
            PlayerPrefs.SetInt("togglestate", 1);
        }
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
