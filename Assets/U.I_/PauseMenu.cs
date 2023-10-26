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

public class PauseMenu : MonoBehaviour, ISelectHandler
{
    public static bool GameIsPause = false;
    public GameObject pauseMenuUI;
    public GameObject optionsPanel;

    private bool isPaused;
    private bool isOptionsPanelOpen;
    public RenderPipelineAsset[] qualityLevels;
    public TMP_Dropdown dropdown;
    private float scrollPosition = 1;
    public AudioMixer audioMixer;
    private Resolution[] resolutions;

    private List<Resolution> filteredResolutions;

    [SerializeField] private TMP_Dropdown resolutionDropdown;

    public ScrollRect scrollRect;

    public float scrollSpeed = 0.1f;

    private int currentResolutionIndex = 0;
    private float currentRefreshRate;
    public PlayerController playerControls;

    public Toggle vSyncToggle;

    public GameObject pauseFirstButton, optionsFirstButton, optionsClosedButton;



    private void Awake()
    {
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

    private void Update()
    {
        float scrollInput = Input.GetAxis("Vertical");  

        if (scrollInput != 0)
        {
            
            float newScrollPosition = scrollRect.verticalNormalizedPosition - scrollInput * scrollSpeed;

            
            newScrollPosition = Mathf.Clamp01(newScrollPosition);

            
            scrollRect.verticalNormalizedPosition = newScrollPosition;
        }
    }

    public void Resume()
    {
       pauseMenuUI.SetActive(false);
       Time.timeScale = 1f;
       isPaused = false;
       isOptionsPanelOpen = false;
       optionsPanel.SetActive(false);

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);

    }

    public void OnSelect(BaseEventData eventData)
    {
            scrollRect.verticalScrollbar.value = scrollPosition;
    }


    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MyExposedParam", volume);
        Debug.Log(volume);
    }

    public void ChangeLevel(int value)
    {
        QualitySettings.SetQualityLevel(value);
        QualitySettings.renderPipeline = qualityLevels[value];
    }


    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
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
