using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInactiveTimer : MonoBehaviour
{
    public float timeUntilInactive;
    private float startTime;

    private void OnEnable()
    {
        startTime = Time.unscaledTime;
    }

    private void Update()
    {
        if (Time.unscaledTime - startTime >= timeUntilInactive)
        {
            Inactive();
        }
    }

    private void Inactive()
    {
        this.gameObject.SetActive(false);
      
    }

}
