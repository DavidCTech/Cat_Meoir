using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalColorblind : MonoBehaviour
{
    public static JournalColorblind instance;

    public Image[] images;
    public Color[] colorsToSwap;
    private Color[] originalColors;

    public TextMeshProUGUI[] textsToSwap;
    public Color[] originalTextColors;
    public Color textColorToSwap;

    public Color defaultImageColor;

    public bool isSwapped = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        StoreOriginalColors();
    }

    public void StoreOriginalColors()
    {
        originalColors = new Color[images.Length];
        originalTextColors = new Color[textsToSwap.Length];

        for (int i = 0; i < images.Length; i++)
        {
            originalColors[i] = images[i].color;
        }

        for (int i = 0; i < textsToSwap.Length; i++)
        {
            originalTextColors[i] = textsToSwap[i].color;
        }
    }

    public void TurnToOriginal(Image image)
    {
        if (!AccessibilityManager.instance.journalColorblindToggle.isOn)
        {
            image.color = defaultImageColor;
        }
    }

    public void SwapColors()
    {
        if (isSwapped)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (i < 11)
                {
                    images[i].color = colorsToSwap[i];
                }
                else if (i > 11 && images[i].sprite == null)
                {
                    images[i].color = colorsToSwap[i];
                }
            }

            for (int i = 0; i < textsToSwap.Length; i++)
            {
                textsToSwap[i].color = textColorToSwap;
            }
        }
        else if (!isSwapped)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (i < 11)
                {
                    images[i].color = originalColors[i];
                }
                else if (i > 11 && images[i].sprite == null)
                {
                    images[i].color = originalColors[i];
                }
            }

            for (int i = 0; i < textsToSwap.Length; i++)
            {
                textsToSwap[i].color = originalTextColors[i];
            }
        }
    }
}
