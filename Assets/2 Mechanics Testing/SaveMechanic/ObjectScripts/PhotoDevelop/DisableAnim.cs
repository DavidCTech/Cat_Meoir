using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAnim : MonoBehaviour
{
    public Animator anim; 
    public void disableAnim()
    {
        anim.enabled = false; 
    }
}
