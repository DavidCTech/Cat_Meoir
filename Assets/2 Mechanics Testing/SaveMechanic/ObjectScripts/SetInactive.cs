using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInactive : MonoBehaviour
{
    public void SetThisInactive()
    {
        this.gameObject.SetActive(false);
    }
}
