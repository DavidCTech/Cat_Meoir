using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingToggles : MonoBehaviour
{
    public Volume postProcessingVolume;

    private FilmGrain filmGrain;
    private MotionBlur motionBlur;

    [Header("Accessibility Toggles")]
    public Toggle filmGrainToggle;
    public Toggle motionBlurToggle;

    private bool isUsingFilmGrain = true, isUsingMotionBlur = true;

    private int filmGrainInt, motionBlurInt;

    public void Awake()
    {
        filmGrainInt = PlayerPrefs.GetInt("FilmGrainState");
        motionBlurInt = PlayerPrefs.GetInt("MotionBlurState");
    }

    public void Start()
    {
        if (PlayerPrefs.HasKey("FilmGrainState"))
        {
            if (filmGrainInt == 1)
            {
                filmGrainToggle.isOn = true;
                isUsingFilmGrain = true;

                if (postProcessingVolume.profile.TryGet(out filmGrain))
                {
                    filmGrain.active = true;
                }
            }
            else
            {
                filmGrainToggle.isOn = false;

                if (postProcessingVolume.profile.TryGet(out filmGrain))
                {
                    filmGrain.active = false;
                }
            }
        }

        if (PlayerPrefs.HasKey("MotionBlurState"))
        {
            if (motionBlurInt == 1)
            {
                motionBlurToggle.isOn = true;
                isUsingMotionBlur = true;

                if (postProcessingVolume.profile.TryGet(out motionBlur))
                {
                    motionBlur.active = true;
                }
            }
            else
            {
                motionBlurToggle.isOn = false;

                if (postProcessingVolume.profile.TryGet(out motionBlur))
                {
                    motionBlur.active = false;
                }
            }
        }
    }

    public void SetFilmGrainMode(bool isUsingFilmGrain)
    {
        filmGrainToggle.isOn = isUsingFilmGrain;
    }

    public void SetMotionBlurMode(bool isUsingMotionBlur)
    {
        motionBlurToggle.isOn = isUsingMotionBlur;
    }

    public void ApplyFilmGrainSettings()
    {
        if (!postProcessingVolume.profile.TryGet(out filmGrain))
        {
            Debug.Log("Film Grain effect not found in the Post Processing Volume.");
            return;
        }

        if (!filmGrainToggle.isOn)
        {
            PlayerPrefs.SetInt("FilmGrainState", 0);
            filmGrain.active = false;
            isUsingFilmGrain = false;
        }
        else
        {
            PlayerPrefs.SetInt("FilmGrainState", 1);
            filmGrain.active = true;
            isUsingFilmGrain = true;
        }
    }

    public void ApplyMotionBlurSettings()
    {
        if (!postProcessingVolume.profile.TryGet(out motionBlur))
        {
            Debug.Log("Motion Blur effect not found in the Post Processing Volume.");
            return;
        }

        if (!motionBlurToggle.isOn)
        {
            PlayerPrefs.SetInt("MotionBlurState", 0);
            motionBlur.active = false;
            isUsingMotionBlur = false;
        }
        else
        {
            PlayerPrefs.SetInt("MotionBlurState", 1);
            motionBlur.active = true;
            isUsingMotionBlur = true;
        }
    }
}
