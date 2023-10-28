using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FPS_Updater : MonoBehaviour
{
    float fps;

    float updateTimer = 0.2f;

    public Toggle fpsToggle;

    [SerializeField] TextMeshProUGUI fpsTitle;

    public void UpdateFPSDisplay()
    {
        updateTimer -= Time.deltaTime;
        if (updateTimer <= 0f)
        {
            fps = 1f / Time.unscaledDeltaTime;
            fpsTitle.text = "FPS: " + Mathf.Round(fps);
            updateTimer = 0.2f;
        }
    }
    
    void Update()
    {
        UpdateFPSDisplay();
    }
}
