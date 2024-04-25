using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsScroll : MonoBehaviour
{
    public float timer, timeAfterTimer;
    public string mainMenu;
    public bool isTimerDone;

    [Header("Fade Screen Variables")]
    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeFromBlack, fadeToBlack;

    void Start()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        FadeFromBlack();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && !isTimerDone)
        {
            isTimerDone = true;
            StartCoroutine(LoadMainMenu());
        }

        if (fadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
            Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0f)
                fadeFromBlack = false;
        }
        else if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
            Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
                fadeToBlack = false;
        }
    }

    public IEnumerator LoadMainMenu()
    {
        FadeToBlack();
        yield return new WaitForSeconds(timeAfterTimer);
        SceneManager.LoadScene(mainMenu);
    }

    public void LoadMenuFromSkip()
    {
        if (!isTimerDone)
        {
            StartCoroutine(LoadMainMenu());
        }
    }

    public void FadeFromBlack()
    {
        fadeFromBlack = true;
        fadeToBlack = false;
    }

    public void FadeToBlack()
    {
        fadeFromBlack = false;
        fadeToBlack = true;
    }
}
