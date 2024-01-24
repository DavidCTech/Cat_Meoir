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


public class PauseMenu : MonoBehaviour, ISelectHandler
{
    public static bool GameIsPause = false;
    public GameObject pauseMenuUI;
    //added in a ref to the photo menu UI
    public GameObject photoMenuUI; 
    public GameObject optionsPanel;

    public Slider sensitivitySlider;
    public CinemachineFreeLook cineCam;
    private CinemachineComposer composerX;


    private bool isPaused;
    private bool isOptionsPanelOpen;
    public RenderPipelineAsset[] qualityLevels;
    public TMP_Dropdown dropdown;
    private float scrollPosition = 1;
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    private int currentResolutionIndex = 0;
    private float currentRefreshRate;

    Resolution[] resolutions;

    private List<Resolution> filteredResolutions;

    [SerializeField] private TMP_Dropdown resolutionDropdown;

    public float scrollSpeed = 0.1f;

    public Toggle fullScreenToggle;
    private int screenInt;

    private bool isFullScreen = false;

    public PlayerController playerControls;

    public Toggle vSyncToggle;

    public GameObject pauseFirstButton, optionsFirstButton, optionsClosedButton;

    const string prefName = "optionsvalue";
    const string resName = "resolutionoption";

    private float sliderValue;

    private void Awake()
    {
        screenInt = PlayerPrefs.GetInt("togglestate");
        if(screenInt == 1)
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

    public void DeactivateMenu()
    {
        pauseMenuUI.SetActive(false);
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
        pauseMenuUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);

    }

    public void ActivateMenu()
    {
        pauseMenuUI.SetActive(true);
        optionsPanel.SetActive(false);
    }


    private void Start()
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
       optionsPanel.SetActive(false);
        sliderValue = volumeSlider.value;
       UnmuteAudio();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        sliderValue = volumeSlider.value;
        MuteAudio();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);

    }

    public void OnSelect(BaseEventData eventData)
    {
            
    }


    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
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

    public void SetVolume(float sliderValue)
    {
        PlayerPrefs.SetFloat("MVolume", sliderValue);
        audioMixer.SetFloat("MyExposedParam", PlayerPrefs.GetFloat("MVolume"));
        audioMixer.SetFloat("MyExposedParam", Mathf.Log10(sliderValue) * 20);
        Debug.Log(sliderValue);
    }

    public void ChangeSensitivity(float value)
    {
        cineCam.m_XAxis.m_MaxSpeed = value;
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
