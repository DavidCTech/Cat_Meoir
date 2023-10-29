using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInactive : MonoBehaviour
{
    public float timeUntilInactive;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Invoke("Inactive", timeUntilInactive);
    }

    private void Inactive()
    {
        this.gameObject.SetActive(false);
    }
}
