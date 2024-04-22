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
using Cinemachine;
using System;

public class GameManager : MonoBehaviour, ISelectHandler
{
    public RenderPipelineAsset[] qualityLevels;
    public TMP_Dropdown dropdown;
    private ScrollRect scrollRect;
    private float scrollPosition = 1;

    public AudioMixer audioMixer;

    public GameObject mainMenuPanel;
    public CanvasGroup controlsPanel;

    public GameObject optionsPanel;

    public GameObject creditsPanel;

    public GameObject audioPanel;

    public GameObject defaultControlsPanel;

    public Slider sensitivitySlider;
    public CinemachineFreeLook cineCam;
    private CinemachineComposer composerX;
    public Slider ySensitivitySlider;

    public Button applyChangesButton;

    Resolution[] resolutions;

    private List<Resolution> filteredResolutions;

    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution previousResolution;

    private int currentResolutionIndex = 0;
    private float currentRefreshRate;

    public Slider volumeSlider;
    public Slider sfxSlider;
    public Slider dialogueSlider;
    public Slider musicSlider;

    public Toggle fullScreenToggle;
    private int screenInt;
    public bool isFullScreen = false;

    public PlayerController playerControls;

    public Toggle fpsToggle;
    public GameObject fpsDisplay;

    public Toggle vSyncToggle;

    public GameObject controlsButton, controlsBackButton, optionsFirstButton, optionsClosedButton, creditsFirstButton, audioFirstButton, audioClosedButton, defaultControlsFirstButton, defaultControlsClosedButton;

    const string prefName = "optionsvalue";
    const string resName = "resolutionoption";

    private bool shouldApplyChanges = true;

    private void Awake()
    {
        screenInt = PlayerPrefs.GetInt("togglestate", 1);
        if (screenInt == 1)
        {
            isFullScreen = true;
            fullScreenToggle.isOn = true;
            SetFullscreen(true);
        }
        else
        {
            fullScreenToggle.isOn = false;
            SetFullscreen(false);
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

        applyChangesButton.onClick.AddListener(ApplyResolutionChanges);

        playerControls = new PlayerController();
    }

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (PlayerPrefs.GetInt("FPSToggleState", 0) == 1)
        {
            fpsToggle.isOn = true;
            ApplyFPS(true);
        }
        else
        {
            fpsToggle.isOn = false;
            ApplyFPS(false);
        }


        if (PlayerPrefs.GetInt("VSyncToggleState", 0) == 1)
        {
            vSyncToggle.isOn = true;
            ApplyVSync(true);
        }
        else
        {
            vSyncToggle.isOn = false;
            ApplyVSync(false);
        }

        if (PlayerPrefs.HasKey("Sensitivity"))
            sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
        else
            sensitivitySlider.value = 0.5f;

        if (PlayerPrefs.HasKey("ySensitivity"))
            ySensitivitySlider.value = PlayerPrefs.GetFloat("ySensitivity");
        else
            ySensitivitySlider.value = 1.0f;

        // Set volume sliders
        if (PlayerPrefs.HasKey("MVolume"))
            volumeSlider.value = PlayerPrefs.GetFloat("MVolume");
        else
            volumeSlider.value = 1.0f;

        // Set music slider
        if (PlayerPrefs.HasKey("MMusic"))
            musicSlider.value = PlayerPrefs.GetFloat("MMusic");
        else
            musicSlider.value = 1.0f;


        if (PlayerPrefs.HasKey("MSfx"))
            sfxSlider.value = PlayerPrefs.GetFloat("MSfx");
        else
            sfxSlider.value = 1.0f;

        if (PlayerPrefs.HasKey("MDialogue"))
            dialogueSlider.value = PlayerPrefs.GetFloat("MDialogue");
        else
            dialogueSlider.value = 1.0f;

        dropdown.value = PlayerPrefs.GetInt(prefName, 3);

        previousResolution = Screen.currentResolution;

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

        int savedResolutionIndex = PlayerPrefs.GetInt(resName, currentResolutionIndex);
        resolutionDropdown.value = savedResolutionIndex;

        resolutionDropdown.RefreshShownValue();
        UpdateResolutionDropdownOptions();
        LoadSavedResolution();
    }


    public void ActivateAudioMenu()
    {
        audioPanel.SetActive(true);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        TurnControlsCanvasOff();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(audioFirstButton);
    }

    public void DeactivateAudioMenu()
    {
        audioPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        TurnControlsCanvasOff();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(audioClosedButton);
    }

    public void DeactivateMenu()
    {
        mainMenuPanel.SetActive(false);
    }

    public void ActivateControlsMenu()
    {
        TurnControlsCanvasOn();
        audioPanel.SetActive(false);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsBackButton);
    }

    public void DeactivateControlsMenu()
    {
        TurnControlsCanvasOff();
        mainMenuPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsButton);
    }

    public void ActivateOptionsMenu()
    {
        optionsPanel.SetActive(true);
        UpdateResolutionDropdownOptions();
        UpdateDropdownLabelText();
        audioPanel.SetActive(false);
        TurnControlsCanvasOff();
        creditsPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void DeactivateOptionsMenu()
    {
        Debug.Log("Close Options Menu pressed");

        // Save changes if "Apply Changes" was pressed
        if (!shouldApplyChanges)
        {
            RevertToPreviousResolution();
        }

        optionsPanel.SetActive(false);
        TurnControlsCanvasOff();
        mainMenuPanel.SetActive(true);
        UpdateResolutionDropdownOptions();
        UpdateDropdownLabelText();




        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }

    public void ActivateCreditsPanel()
    {

        creditsPanel.SetActive(true);
        optionsPanel.SetActive(false);
        audioPanel.SetActive(false);
        TurnControlsCanvasOff();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsFirstButton);
    }

    public void ActivateDefaultControlsPanel()
    {
        defaultControlsPanel.SetActive(true);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        audioPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(defaultControlsFirstButton);
    }

    public void DeactivateDefaultControlsPanel()
    {
        defaultControlsPanel.SetActive(false);
        optionsPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(defaultControlsClosedButton);
    }

    public void ActivateMenu()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        audioPanel.SetActive(false);
        TurnControlsCanvasOff();
    }

    public void OnSelect(BaseEventData eventData)
    {

    }

    public void TurnControlsCanvasOn()
    {
        controlsPanel.alpha = 1;
        controlsPanel.interactable = true;
        controlsPanel.blocksRaycasts = true;
    }

    public void TurnControlsCanvasOff()
    {
        controlsPanel.alpha = 0;
        controlsPanel.interactable = false;
        controlsPanel.blocksRaycasts = false;
    }



    private void LoadSavedResolution()
    {
        int savedResolutionIndex = PlayerPrefs.GetInt(resName, -1);
        if (savedResolutionIndex >= 0 && savedResolutionIndex < resolutions.Length)
        {
            resolutionDropdown.value = savedResolutionIndex;
            resolutionDropdown.RefreshShownValue();
            previousResolution = resolutions[savedResolutionIndex];
            UpdateDropdownLabelText();
        }
    }

    private int GetSavedResolutionIndex()
    {
        Resolution currentResolution = Screen.currentResolution;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (IsResolutionEqual(resolutions[i], previousResolution))
            {
                return i;
            }
        }
        return -1;
    }

    public void ApplyResolutionChanges()
    {
        int selectedResolutionIndex = resolutionDropdown.value;
        previousResolution = resolutions[selectedResolutionIndex];

        // Set and apply the selected resolution
        SetResolution(selectedResolutionIndex);


        shouldApplyChanges = true;

        // Save the selected resolution index
        PlayerPrefs.SetInt(resName, selectedResolutionIndex);
        PlayerPrefs.Save();

        Debug.Log("Apply Changes pressed");
    }

    private void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Debug.Log("Setting resolution: " + resolution.width + "x" + resolution.height + " " + resolution.refreshRate + "Hz");

        // Update the previousResolution variable
        previousResolution = resolution;

        // Apply the resolution
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private bool IsResolutionEqual(Resolution resolution1, Resolution resolution2)
    {
        return resolution1.width == resolution2.width &&
               resolution1.height == resolution2.height &&
               resolution1.refreshRate == resolution2.refreshRate;
    }


    private void RevertToPreviousResolution()
    {
        int currentResolutionIndex = GetSavedResolutionIndex();
        SetResolution(GetSavedResolutionIndex());
        UpdateDropdownLabelText();
    }

    private void UpdateResolutionDropdownOptions()
    {
        Debug.Log("Updating dropdown options");

        // Obtain available resolutions
        resolutions = Screen.resolutions;

        // Your code to update resolution options in the dropdown goes here
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionOption = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            options.Add(resolutionOption);
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        // Find the index of the current resolution in the list of available resolutions
        int currentResolutionIndex = -1;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (IsResolutionEqual(resolutions[i], Screen.currentResolution))
            {
                currentResolutionIndex = i;
                break;
            }
        }

        // If the current resolution is not found, use the last known index or the default index (0)
        if (currentResolutionIndex == -1)
        {
            currentResolutionIndex = PlayerPrefs.GetInt(resName, 0);
        }

        // Ensure that the selected resolution index is within the bounds
        currentResolutionIndex = Mathf.Clamp(currentResolutionIndex, 0, resolutions.Length - 1);

        // Set the dropdown value to the selected resolution index
        resolutionDropdown.value = currentResolutionIndex;

        // Refresh the shown value
        resolutionDropdown.RefreshShownValue();
    }

    private void UpdateDropdownLabelText()
    {
        int selectedResolutionIndex = resolutionDropdown.value;
        string selectedResolutionText = $"{resolutions[selectedResolutionIndex].width}x{resolutions[selectedResolutionIndex].height} {resolutions[selectedResolutionIndex].refreshRate}Hz";
        TextMeshProUGUI dropdownLabel = resolutionDropdown.GetComponentInChildren<TextMeshProUGUI>();
        if (dropdownLabel != null)
        {
            dropdownLabel.text = selectedResolutionText;
            Debug.Log("Updated Label Text: " + dropdownLabel.text);
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found in the TMPro dropdown.");
        }
    }

    private string GetResolutionLabel(int resolutionIndex)
    {
        if (resolutionIndex >= 0 && resolutionIndex < resolutions.Length)
        {
            Resolution resolution = resolutions[resolutionIndex];
            return resolution.width + "x" + resolution.height + " " + resolution.refreshRate + "Hz";
        }
        else
        {
            return "Unknown Resolution";
        }
    }

    public void SetMaster(float sliderValue)
    {
        PlayerPrefs.SetFloat("MVolume", sliderValue);
        audioMixer.SetFloat("MyExposedParam", PlayerPrefs.GetFloat("MVolume"));
        audioMixer.SetFloat("MyExposedParam", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFX(float sliderValue)
    {
        PlayerPrefs.SetFloat("MSfx", sliderValue);
        audioMixer.SetFloat("MyExposedParam 2", PlayerPrefs.GetFloat("MSfx"));
        audioMixer.SetFloat("MyExposedParam 2", Mathf.Log10(sliderValue) * 20);
        Debug.Log(sliderValue);
    }

    public void SetDialogue(float sliderValue)
    {
        PlayerPrefs.SetFloat("MDialogue", sliderValue);
        audioMixer.SetFloat("MyExposedParam 3", PlayerPrefs.GetFloat("MDialogue"));
        audioMixer.SetFloat("MyExposedParam 3", Mathf.Log10(sliderValue) * 20);
        Debug.Log(sliderValue);
    }

    public void SetMusic(float sliderValue)
    {
        PlayerPrefs.SetFloat("MMusic", sliderValue);
        audioMixer.SetFloat("MyExposedParam 1", PlayerPrefs.GetFloat("MMusic"));
        audioMixer.SetFloat("MyExposedParam 1", Mathf.Log10(sliderValue) * 20);
        Debug.Log(sliderValue);
    }

    public void ChangeSensitivity(float value)
    {
        PlayerPrefs.SetFloat("Sensitivity", value);
        PlayerPrefs.Save();
        cineCam.m_XAxis.m_MaxSpeed = value;
    }

    public void ChangeYSensitivity(float value)
    {
        PlayerPrefs.SetFloat("ySensitivity", value);
        PlayerPrefs.Save();
        cineCam.m_YAxis.m_MaxSpeed = value;
    }


    public void ChangeLevel(int value)
    {
        QualitySettings.SetQualityLevel(value);
        QualitySettings.renderPipeline = qualityLevels[value];
    }


    public void SetFullscreen(bool isFullscreen)
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


    public void ApplyVSync(bool isVSyncOn)
    {
        QualitySettings.vSyncCount = isVSyncOn ? 1 : 0;

        PlayerPrefs.SetInt("VSyncToggleState", isVSyncOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ApplyFPS(bool isFPSEnabled)
    {
        fpsDisplay.SetActive(isFPSEnabled); // Activate or deactivate the FPS display GameObject

        // Save the toggle state to PlayerPrefs
        PlayerPrefs.SetInt("FPSToggleState", isFPSEnabled ? 1 : 0);
        PlayerPrefs.Save(); // Save PlayerPrefs immediately
    }


    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();

    }
}
