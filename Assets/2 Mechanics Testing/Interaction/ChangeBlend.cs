using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeBlend : MonoBehaviour
{
    private CinemachineBrain brain;

    void Start()
    {
        brain = this.gameObject.GetComponent<CinemachineBrain>();
    }
    public void smooth()
    {
       
        brain.m_DefaultBlend.m_Time = 10f; // 0 Time equals a cut
    }
    public void cut()
    {
       
        brain.m_DefaultBlend.m_Time = 0f; // 0 Time equals a cut
    }
}
