using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GammaSlider : MonoBehaviour
{
    [Header("Gamma Slider and Panel Variables")]
    public Slider gammaSlider;
    public Image pawPrintPanelImage;
    public Image gammaPanel;
    public CanvasGroup gammaPanelCanvas;

    void Awake()
    {

        if (PlayerPrefs.HasKey("GammaValue"))
        {
            gammaSlider.value = PlayerPrefs.GetFloat("GammaValue");

            if (pawPrintPanelImage != null)
            {
                Color color = pawPrintPanelImage.color;
                color.a = gammaSlider.value;
                pawPrintPanelImage.color = color;
            }

            if (gammaPanel != null)
            {
                Color gammaColor = gammaPanel.color;
                gammaColor.a = gammaSlider.value;
                gammaPanel.color = gammaColor;
                gammaPanelCanvas.alpha = 1;
            }
        }
        else
        {
            if (pawPrintPanelImage != null)
            {
                Color color = pawPrintPanelImage.color;
                color.a = 0f;
                pawPrintPanelImage.color = color;
            }

            if (gammaPanel != null)
            {
                Color gammaColor = gammaPanel.color;
                gammaColor.a = 0f;
                gammaPanel.color = gammaColor;
                gammaPanelCanvas.alpha = 0;
            }
        }
    }

    public void AdjustGammaSlider(float gamma)
    {
        Mathf.Clamp(0f, 0.9f, gamma);
        PlayerPrefs.SetFloat("GammaValue", gamma);

        if (pawPrintPanelImage != null)
        {
            Color color = pawPrintPanelImage.color;
            color.a = gamma;
            pawPrintPanelImage.color = color;
        }

        if (gammaPanel != null)
        {
            Color gammaColor = gammaPanel.color;
            gammaColor.a = gamma;
            gammaPanel.color = gammaColor;
        }
    }
}
