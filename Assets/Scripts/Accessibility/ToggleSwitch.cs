using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour
{
    public static ToggleSwitch instance;

    public Toggle[] shadowToggles;
    public int shadowActiveIndex = 0;
    private int shadowColorInt;

    public Toggle[] npcToggles;
    public int npcActiveIndex = 0;
    private int npcColorInt;

    public Toggle[] enemyToggles;
    public int enemyActiveIndex = 0;
    private int enemyColorInt;


    void Awake()
    {
        //Make script an instance
        if (instance == null)
        {
            instance = this;
        }

        shadowColorInt = PlayerPrefs.GetInt("ShadowColor");
        npcColorInt = PlayerPrefs.GetInt("NpcColor");
        enemyColorInt = PlayerPrefs.GetInt("EnemyColor");
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("ShadowColor"))
        {
            shadowActiveIndex = PlayerPrefs.GetInt("ShadowColor");
            ActivateToggle(shadowActiveIndex);
        }
        else
        {
            shadowActiveIndex = 4;
            PlayerPrefs.SetInt("ShadowColor", shadowActiveIndex);
            ActivateToggle(shadowActiveIndex);
        }

        if (PlayerPrefs.HasKey("NpcColor"))
        {
            npcActiveIndex = PlayerPrefs.GetInt("NpcColor");
            ActivateNpcToggle(npcActiveIndex);
        }
        else
        {
            npcActiveIndex = 3;
            PlayerPrefs.SetInt("NpcColor", npcActiveIndex);
            ActivateNpcToggle(npcActiveIndex);
        }

        if (PlayerPrefs.HasKey("EnemyColor"))
        {
            enemyActiveIndex = PlayerPrefs.GetInt("EnemyColor");
            ActivateEnemyToggle(enemyActiveIndex);
        }
        else
        {
            enemyActiveIndex = 0;
            PlayerPrefs.SetInt("EnemyColor", enemyActiveIndex);
            ActivateEnemyToggle(enemyActiveIndex);
        }

        // Add listeners to the onValueChanged event of each toggle
        for (int i = 0; i < shadowToggles.Length; i++)
        {
            int index = i; // Store current index for closure
            shadowToggles[i].onValueChanged.AddListener(newValue => OnToggleValueChanged(index, newValue));
        }

        // Add listeners to the onValueChanged event of each toggle
        for (int i = 0; i < npcToggles.Length; i++)
        {
            int index = i; // Store current index for closure
            npcToggles[i].onValueChanged.AddListener(newValue => OnToggleNpcValueChanged(index, newValue));
        }

        // Add listeners to the onValueChanged event of each toggle
        for (int i = 0; i < enemyToggles.Length; i++)
        {
            int index = i; // Store current index for closure
            enemyToggles[i].onValueChanged.AddListener(newValue => OnToggleEnemyValueChanged(index, newValue));
        }
    }

    void OnToggleValueChanged(int index, bool newValue)
    {
        // When a toggle is clicked, activate it and deactivate all others
        if (newValue)
        {
            ActivateToggle(index);
        }
    }

    void OnToggleNpcValueChanged(int index, bool newValue)
    {
        // When a toggle is clicked, activate it and deactivate all others
        if (newValue)
        {
            ActivateNpcToggle(index);
        }
    }

    void OnToggleEnemyValueChanged(int index, bool newValue)
    {
        // When a toggle is clicked, activate it and deactivate all others
        if (newValue)
        {
            ActivateEnemyToggle(index);
        }
    }

    public void ActivateToggle(int indexToActivate)
    {
        // Activate the selected toggle and deactivate all others
        for (int i = 0; i < shadowToggles.Length; i++)
        {
            // Ensure the toggle at index i exists
            if (shadowToggles[i] != null)
            {
                shadowToggles[i].isOn = i == indexToActivate; // Activate the selected toggle, deactivate others
                shadowActiveIndex = indexToActivate;
                PlayerPrefs.SetInt("ShadowColor", shadowActiveIndex);
            }
            else
            {
                Debug.LogError("Toggle at index " + i + " is null.");
            }
        }
    }

    public void ActivateNpcToggle(int indexToActivate)
    {
        // Activate the selected toggle and deactivate all others
        for (int i = 0; i < npcToggles.Length; i++)
        {
            // Ensure the toggle at index i exists
            if (npcToggles[i] != null)
            {
                npcToggles[i].isOn = i == indexToActivate; // Activate the selected toggle, deactivate others
                npcActiveIndex = indexToActivate;
                PlayerPrefs.SetInt("NpcColor", npcActiveIndex);
            }
            else
            {
                Debug.LogError("Toggle at index " + i + " is null.");
            }
        }
    }

    public void ActivateEnemyToggle(int indexToActivate)
    {
        // Activate the selected toggle and deactivate all others
        for (int i = 0; i < enemyToggles.Length; i++)
        {
            // Ensure the toggle at index i exists
            if (enemyToggles[i] != null)
            {
                enemyToggles[i].isOn = i == indexToActivate; // Activate the selected toggle, deactivate others
                enemyActiveIndex = indexToActivate;
                PlayerPrefs.SetInt("EnemyColor", enemyActiveIndex);
            }
            else
            {
                Debug.LogError("Toggle at index " + i + " is null.");
            }
        }
    }
}
