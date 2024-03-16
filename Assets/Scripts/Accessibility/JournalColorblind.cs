using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class JournalColorblind : MonoBehaviour
{
    public static JournalColorblind instance;

    public Image[] images;
    public Color[] colorsToSwap;
    private Color[] originalColors;

    public TextMeshProUGUI[] textsToSwap;
    public Color[] originalTextColors;
    public Color textColorToSwap;

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

    public void SwapColors()
    {
        if (!isSwapped)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].color = colorsToSwap[i];
            }

            for (int i = 0; i < textsToSwap.Length; i++)
            {
                textsToSwap[i].color = textColorToSwap;
            }

            isSwapped = true;
        }
        else
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].color = originalColors[i];
            }

            for (int i = 0; i < textsToSwap.Length; i++)
            {
                textsToSwap[i].color = originalTextColors[i];
            }

            isSwapped = false;
        }
    }
}
