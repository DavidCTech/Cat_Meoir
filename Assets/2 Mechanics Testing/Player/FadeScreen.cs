using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public GameObject fadeScreen;

    public float fadeDuration = 1.5f; // Duration of the fade effect in seconds
    public Color startColor = Color.clear; // Starting color (usually transparent)
    public Color endColor = Color.black; // Target color (black in this case)

    private Image fadeImage;
    private float timer;
    private bool Fading = false;

    void Start()
    {
        fadeScreen.SetActive(false);
        fadeImage = GetComponent<Image>();
        fadeImage.color = startColor; // Set the initial color of the image
        //StartFade();
    }

    public void StartFade()
    {
        fadeScreen.SetActive(true);
        Fading = true;
        timer = 0f;
    }

    void Update()
    {
        fadeImage.fillAmount += 0.1f * Time.unscaledDeltaTime;
        if (Fading)
        {
            timer += Time.deltaTime;
            float normalizedTime = timer / fadeDuration;

            fadeImage.color = Color.Lerp(startColor, endColor, normalizedTime);

            if (normalizedTime >= 1.0f)
            {
                Fading = false;
            }
        }
    }
}
