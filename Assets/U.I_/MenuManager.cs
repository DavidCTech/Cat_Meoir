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

    public GameObject confirmationPopupPanel;

    public ConfirmationPopup confirmationPopup;

    public Slider volumeSlider;
    public Slider sfxSlider;
    public Slider dialogueSlider;
    public Slider musicSlider;

    public Toggle fullScreenToggle;
    private int screenInt;
    public bool isFullScreen = false;

    public PlayerController playerControls;

    public Toggle vSyncToggle;

    public GameObject controlsButton, controlsBackButton, optionsFirstButton, optionsClosedButton, creditsFirstButton, audioFirstButton, audioClosedButton;

    const string prefName = "optionsvalue";
    const string resName = "resolutionoption";

    private bool shouldApplyChanges = true;

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

        applyChangesButton.onClick.AddListener(ApplyResolutionChanges);

        playerControls = new PlayerController();
    }

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
        volumeSlider.value = PlayerPrefs.GetFloat("MVolume");
        ySensitivitySlider.value = PlayerPrefs.GetFloat("ySensitivity");

        musicSlider.value = PlayerPrefs.GetFloat("MMusic");

        volumeSlider.value = PlayerPrefs.GetFloat("MVolume");

        sfxSlider.value = PlayerPrefs.GetFloat("MSfx");

        dialogueSlider.value = PlayerPrefs.GetFloat("MDialogue");

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
        if (shouldApplyChanges)
        {
            // Save the selected resolution index
            int selectedResolutionIndex = resolutionDropdown.value;
            PlayerPrefs.SetInt(resName, selectedResolutionIndex);
            PlayerPrefs.Save();
            Debug.Log("Changes saved");

            // Reset the flag
            shouldApplyChanges = false;
        }
        else
        {
            // Revert to the previous resolution if "Apply Changes" was not pressed
            RevertToPreviousResolution();
        }

        optionsPanel.SetActive(false);
        TurnControlsCanvasOff();
        mainMenuPanel.SetActive(true);

        int savedResolutionIndex = PlayerPrefs.GetInt(resName, currentResolutionIndex);
        resolutionDropdown.value = savedResolutionIndex;
        resolutionDropdown.RefreshShownValue();

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

    public void ApplyResolutionChanges()
    {
        int selectedResolutionIndex = resolutionDropdown.value;
        previousResolution = resolutions[selectedResolutionIndex];

        SetResolution(selectedResolutionIndex);

        UpdateResolutionDropdownOptions();
        shouldApplyChanges = true;

        Debug.Log("Apply Changes pressed");



        // Save the selected resolution index only if shouldApplyChanges is true
        if (shouldApplyChanges)
        {
            PlayerPrefs.SetInt(resName, selectedResolutionIndex);
            PlayerPrefs.Save();
        }

       
    }

    private void SetResolution(int resolutionIndex)
    {
        Debug.Log("Setting resolution to index: " + resolutionIndex);

        // Save the selected resolution index
        PlayerPrefs.SetInt(resName, resolutionIndex);
        PlayerPrefs.Save();

        // Debug logs to check PlayerPrefs values
        Debug.Log("PlayerPrefs " + resName + " after save: " + PlayerPrefs.GetInt(resName));

        // Apply the resolution
        Resolution resolution = resolutions[resolutionIndex];
        Debug.Log("Applying resolution: " + resolution.width + "x" + resolution.height + " " + resolution.refreshRate + "Hz");
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private bool IsResolutionEqual(Resolution resolution1, Resolution resolution2)
    {
        return resolution1.width == resolution2.width &&
               resolution1.height == resolution2.height &&
               resolution1.refreshRate == resolution2.refreshRate;
    }


    public void RevertToPreviousResolution()
    {
        Resolution currentResolution = Screen.currentResolution;

        Screen.SetResolution(previousResolution.width, previousResolution.height, Screen.fullScreen);

        Debug.Log("Reverting to previous resolution");

        // Log additional information if needed
        Debug.Log("Previous resolution: " + previousResolution);

        // Check if resolution changed before updating dropdown
        if (!IsResolutionEqual(currentResolution, previousResolution))
        {
            UpdateResolutionDropdownOptions();

        }

    }
    private void UpdateResolutionDropdownOptions()
    {
        Debug.Log("Updating dropdown options");
        // Your code to update resolution options in the dropdown goes here
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionOption = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            options.Add(resolutionOption);
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        int selectedResolutionIndex = Mathf.Clamp(PlayerPrefs.GetInt(resName, 0), 0, resolutions.Length - 1);

        resolutionDropdown.value = selectedResolutionIndex;

        resolutionDropdown.RefreshShownValue();


        string selectedResolutionText = options[selectedResolutionIndex];

        TextMeshProUGUI dropdownLabel = resolutionDropdown.GetComponentInChildren<TextMeshProUGUI>();
        if (dropdownLabel != null)
        {
            
            dropdownLabel.text = selectedResolutionText;
            Debug.Log("Current Label Text: " + dropdownLabel.text);

            // Update the label text
            dropdownLabel.text = selectedResolutionText;

            // Log the updated text
            Debug.Log("Updated Label Text: " + dropdownLabel.text);

        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found in the TMPro dropdown.");
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
