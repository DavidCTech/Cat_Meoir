using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSavedText : MonoBehaviour
{
    public GameObject settingsSavedText;
    public float timer;

    public void Clicked()
    {
        StartCoroutine(DisplayTextCo());
    }

    public IEnumerator DisplayTextCo()
    {
        settingsSavedText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(timer);
        settingsSavedText.gameObject.SetActive(false);
    }
}
