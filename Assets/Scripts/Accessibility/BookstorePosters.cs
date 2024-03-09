using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BookstorePosters : MonoBehaviour
{
    public static BookstorePosters instance;

    public Material newMaterial;
    private Material[] originalMaterials;
    public GameObject[] objectsToSwap;

    public Color newColor;
    private Color[] originalColors;
    public Image[] imagesToSwap;
    public TextMeshProUGUI[] textsToSwap;

    private bool isSwapped = false;

    //Make script an instance
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        StoreOriginalMaterials();
        StoreOriginalColors();
    }

    public void StoreOriginalMaterials()
    {
        originalMaterials = new Material[objectsToSwap.Length];

        for (int i = 0; i < objectsToSwap.Length; i++)
        {
            Renderer renderer = objectsToSwap[i].GetComponent<Renderer>();

            if (renderer != null)
            {
                originalMaterials[i] = renderer.sharedMaterial;
            }
        }
    }

    public void StoreOriginalColors()
    {
        originalColors = new Color[imagesToSwap.Length + textsToSwap.Length];

        for (int i = 0; i < imagesToSwap.Length; i++)
        {
            originalColors[i] = imagesToSwap[i].color;
        }

        for (int i = 0; i < textsToSwap.Length; i++)
        {
            originalColors[i + imagesToSwap.Length] = textsToSwap[i].color;
        }
    }

    public void SwapMaterials()
    {
        if (!isSwapped)
        {
            foreach (GameObject gameObject in objectsToSwap)
            {
                Renderer renderer = gameObject.GetComponent<Renderer>();

                if (renderer != null)
                {
                    renderer.material = newMaterial;
                }
            }

            foreach (Image image in imagesToSwap)
            {
                image.color = newColor;
            }

            foreach (TextMeshProUGUI text in textsToSwap)
            {
                text.color = newColor;
            }

            isSwapped = true;
        }
        else
        {
            for (int i = 0; i < objectsToSwap.Length; i++)
            {
                Renderer renderer = objectsToSwap[i].GetComponent<Renderer>();

                if (renderer != null && originalMaterials[i] != null)
                {
                    renderer.material = originalMaterials[i];
                }
            }

            int index = 0;

            foreach (Image image in imagesToSwap)
            {
                image.color = originalColors[index];
                index++;
            }

            foreach (TextMeshProUGUI text in textsToSwap)
            {
                text.color = originalColors[index];
                index++;
            }

            isSwapped = false;
        }
    }
}
