using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.Events;
using System;


public class PauseMenu : MonoBehaviour, ISelectHandler
{
    public static bool GameIsPause = false;
    public GameObject pauseMenuUI;
    //added in a ref to the photo menu UI
    public GameObject photoMenuUI;
    public GameObject optionsPanel;
    public GameObject audioPanel;

    public Slider sensitivitySlider;
    public CinemachineFreeLook cineCam;
    private CinemachineComposer composerX;
    public Slider ySensitivitySlider;


    private bool isPaused;
    private bool isOptionsPanelOpen;
    private bool isAudioPanelOpen;

    public RenderPipelineAsset[] qualityLevels;
    public TMP_Dropdown dropdown;
    private float scrollPosition = 1;
    public AudioMixer audioMixer;

    public Slider volumeSlider;
    public Slider sfxSlider;
    public Slider dialogueSlider;
    public Slider musicSlider;

    public Button applyChangesButton;

    private Resolution[] resolutions;

    private List<Resolution> filteredResolutions;

    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution previousResolution;

    private int currentResolutionIndex = 0;
    private float currentRefreshRate;

    private bool shouldApplyChanges = true;

    public float scrollSpeed = 0.1f;

    public Toggle fullScreenToggle;
    private int screenInt;

    private bool isFullScreen = false;

    public PlayerController playerControls;

    public Toggle vSyncToggle;

    public GameObject pauseFirstButton, optionsFirstButton, optionsClosedButton, audioFirstButton, audioClosedButton;

    const string prefName = "optionsvalue";
    const string resName = "resolutionoption";

    private float sliderValue;

    [Header("Peyton's Stuff")]
    public CanvasGroup accessibilityMenu;
    public CanvasGroup rebindingMenu;
    public CanvasGroup gammaMenu;
    public GameObject controlsBackButton;
    public GammaSlider gammaSlider;

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
    public void OnEnable()
    {
        playerControls.Player.MenuOpenClose.performed += OnMenuOpenClose;
        playerControls.Player.MenuOpenClose.Enable();
    }
    public void OnDisable()
    {
        playerControls.Player.MenuOpenClose.performed -= OnMenuOpenClose;
        playerControls.Player.MenuOpenClose.Disable();

    }

    public void OnMenuOpenClose(InputAction.CallbackContext context)
    {
        Debug.Log("Pause");

        if (!isPaused && !isOptionsPanelOpen)
        {
            Pause();
        }

        else if (isPaused || isOptionsPanelOpen)
        {
            Resume();
        }
    }

    void OpenOptionsPanel()
    {
        isOptionsPanelOpen = true;
    }

    void OpenAudioPanel()
    {
        isAudioPanelOpen = true;
    }

    public void ActivateAudioMenu()
    {
        audioPanel.SetActive(true);
        DeactivateRebindingMenu();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(audioFirstButton);
    }

    public void DeactivateAudioMenu()
    {
        audioPanel.SetActive(false);
        pauseMenuUI.SetActive(true);
        DeactivateRebindingMenu();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(audioClosedButton);
    }

    public void DeactivateMenu()
    {
        pauseMenuUI.SetActive(false);
    }

    public void ActivateOptionsMenu()
    {
        optionsPanel.SetActive(true);
        UpdateResolutionDropdownOptions();
        DeactivateRebindingMenu();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void DeactivateOptionsMenu()
    {
        Debug.Log("Close Options Menu pressed");

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
            UpdateResolutionDropdownOptions();
        }

        optionsPanel.SetActive(false);
        pauseMenuUI.SetActive(true);
        resolutionDropdown.RefreshShownValue();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }

    //Peyton's Functions
    public void ActivateControlsMenu()
    {
        ActivateRebindingMenu();
        pauseMenuUI.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsBackButton);
    }

    public void DeactivateControlsMenu()
    {
        DeactivateRebindingMenu();
        pauseMenuUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }

    public void ChangeToControlBackButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsBackButton);
    }

    public void ActivateRebindingMenu()
    {
        if (rebindingMenu.alpha == 0)
        {
            rebindingMenu.alpha = 1;
            rebindingMenu.interactable = true;
            rebindingMenu.blocksRaycasts = true;
        }
    }

    public void DeactivateRebindingMenu()
    {
        if (rebindingMenu.alpha == 1)
        {
            rebindingMenu.alpha = 0;
            rebindingMenu.interactable = false;
            rebindingMenu.blocksRaycasts = false;
        }
    }

    public void DeactivateAccessibilityMenu()
    {
        if (accessibilityMenu.alpha == 1)
        {
            accessibilityMenu.alpha = 0;
            accessibilityMenu.interactable = false;
            accessibilityMenu.blocksRaycasts = false;
        }
    }

    public void DeactivateGammaMenu()
    {
        if (gammaMenu.alpha == 1)
        {
            gammaMenu.alpha = 0;
            gammaMenu.interactable = false;
            gammaMenu.blocksRaycasts = false;
        }
    }

    public void ActivateMenu()
    {
        pauseMenuUI.SetActive(true);
        optionsPanel.SetActive(false);
        audioPanel.SetActive(false);
        DeactivateControlsMenu();
        DeactivateAccessibilityMenu();
    }

    private void Start()
    {
        ySensitivitySlider.value = PlayerPrefs.GetFloat("ySensitivity");

        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");

        volumeSlider.value = PlayerPrefs.GetFloat("MVolume");

        sfxSlider.value = PlayerPrefs.GetFloat("MSfx");

        dialogueSlider.value = PlayerPrefs.GetFloat("MDialogue");

        musicSlider.value = PlayerPrefs.GetFloat("MMusic");

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

        gammaSlider = FindAnyObjectByType<GammaSlider>();
    }

    private void Update()
    {

    }

    public void Resume()
    {
        //turned off the photo menu ui 
        photoMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        isOptionsPanelOpen = false;
        isAudioPanelOpen = false;
        optionsPanel.SetActive(false);
        audioPanel.SetActive(false);
        sliderValue = volumeSlider.value;
        UnmuteAudio();

        if (rebindingMenu.alpha == 1 || accessibilityMenu.alpha == 1 || gammaMenu.alpha == 1)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        rebindingMenu.gameObject.SetActive(false);
        DeactivateRebindingMenu();
        DeactivateAccessibilityMenu();
        DeactivateGammaMenu();
        if (gammaSlider != null)
            GammaSlider.instance.EnableGammaPanel();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        sliderValue = volumeSlider.value;
        MuteAudio();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        rebindingMenu.gameObject.SetActive(true);
        DeactivateRebindingMenu();
        DeactivateAccessibilityMenu();
        DeactivateGammaMenu();
        if (gammaSlider != null)
            GammaSlider.instance.DisableGammaPanel();
    }

    public void OnSelect(BaseEventData eventData)
    {

    }

    public void ApplyResolutionChanges()
    {
        int selectedResolutionIndex = resolutionDropdown.value;
        previousResolution = resolutions[selectedResolutionIndex];

        SetResolution(selectedResolutionIndex);

        Debug.Log("Apply Changes pressed");

        // Update the dropdown options regardless of whether changes are applied or not
        UpdateResolutionDropdownOptions();

        // Save the selected resolution index only if shouldApplyChanges is true
        if (shouldApplyChanges)
        {
            PlayerPrefs.SetInt(resName, selectedResolutionIndex);
            PlayerPrefs.Save();
            shouldApplyChanges = false; // Reset the flag after saving changes
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

        TextMeshProUGUI dropdownLabel = resolutionDropdown.GetComponentInChildren<TextMeshProUGUI>();
        if (dropdownLabel != null)
        {
            dropdownLabel.text = resolutionDropdown.options[resolutionIndex].text;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found in the TMPro dropdown.");
        }

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

        // Ensure that the selected resolution index is within the bounds
        int selectedResolutionIndex = Mathf.Clamp(PlayerPrefs.GetInt(resName, 0), 0, resolutions.Length - 1);

        // Set the dropdown value to the selected resolution index
        resolutionDropdown.value = selectedResolutionIndex;

        // Refresh the shown value
        resolutionDropdown.RefreshShownValue();

        if (shouldApplyChanges)
        {
            TextMeshProUGUI dropdownLabel = resolutionDropdown.GetComponentInChildren<TextMeshProUGUI>();
            if (dropdownLabel != null)
            {
                dropdownLabel.text = options[selectedResolutionIndex];
                Debug.Log("Updated Label Text: " + dropdownLabel.text);
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found in the TMPro dropdown.");
            }
        }

    }



    void MuteAudio()
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("MyExposedParam", -80f);
        }
    }

    void UnmuteAudio()
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("MyExposedParam", Mathf.Log10(sliderValue) * 20);
        }
    }

    public void SetMaster(float sliderValue)
    {
        PlayerPrefs.SetFloat("MVolume", sliderValue);
        audioMixer.SetFloat("MyExposedParam", PlayerPrefs.GetFloat("MVolume"));
        audioMixer.SetFloat("MyExposedParam", Mathf.Log10(sliderValue) * 20);
        Debug.Log(sliderValue);
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

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
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
}



