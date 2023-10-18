using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlackAndWhiteToggle : MonoBehaviour
{
    private Volume volume;
    private FilmGrain filmGrain;

    private void Awake()
    {
        volume = GetComponent<Volume>();

        if (volume == null)
        {
            Debug.LogError("Volume component not found on this GameObject.");
            return;
        }

        if (volume.profile.TryGet(out filmGrain))
        {

        }
        else
        {
            Debug.LogError("FilmGrain component not found in the Volume.");
        }
    }

    //originally I thought it was desaturate so I labedled the method as that but its grain? 
    public void Desaturate()
    {
        bool isInNoPostProcessingLayer = gameObject.layer == LayerMask.NameToLayer("NoPostProcessing");
        if (!isInNoPostProcessingLayer)
        {
            filmGrain.intensity.value = 1.0f; // Enable the FilmGrain effect for a black and white look
        }

    }
    //same with saturate 
    public void Saturate()
    {
        filmGrain.intensity.value = 0.0f;
    }
   
}