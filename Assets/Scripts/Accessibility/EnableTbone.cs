using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTbone : MonoBehaviour
{
    public GameObject tBone;
    public float timer;

    void Awake()
    {
        StartCoroutine(EnableDisableCo());
    }

    public IEnumerator EnableDisableCo()
    {
        tBone.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(timer);
        tBone.gameObject.SetActive(false);
    }
}
