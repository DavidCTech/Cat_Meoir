using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GammaSlider : MonoBehaviour
{
    public Slider gammaSlider;

    public Image pawPrintPanelImage;
    public Image gammaPanel;

    void Start()
    {
        if (PlayerPrefs.HasKey("GammaValue"))
        {
            gammaSlider.value = PlayerPrefs.GetFloat("GammaValue");
            Color color = pawPrintPanelImage.color;
            color.a = gammaSlider.value;
            pawPrintPanelImage.color = color;

            Color gammaColor = gammaPanel.color;
            color.a = gammaSlider.value;
            gammaPanel.color = color;
        }
        else
        {
            Color color = pawPrintPanelImage.color;
            color.a = 0f;
            pawPrintPanelImage.color = color;

            Color gammaColor = gammaPanel.color;
            color.a = 0f;
            gammaPanel.color = color;
        }
    }

    void Update()
    {

    }

    public void AdjustGammaSlider(float gamma)
    {
        Mathf.Clamp(0f, 0.9f, gamma);
        PlayerPrefs.SetFloat("GammaValue", gamma);
        Color color = pawPrintPanelImage.color;
        color.a = gamma;
        pawPrintPanelImage.color = color;

        Color gammaColor = gammaPanel.color;
        color.a = gamma;
        gammaPanel.color = color;

    }
}
