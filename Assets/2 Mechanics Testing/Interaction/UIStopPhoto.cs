using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStopPhoto : MonoBehaviour
{
    public UICursor uiCursor;
    void OnEnable()
    {
        if (uiCursor != null)
        {
            uiCursor.ignore = true;
        }
    }


    void OnDisable()
    {
        if (uiCursor != null)
        {
            uiCursor.ignore = false;
        }
    }
}
