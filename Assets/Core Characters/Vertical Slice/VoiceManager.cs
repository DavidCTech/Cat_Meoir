using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{

    public GameObject policeVS;
    public Human_AI voiceHuman;

    void Start()
    {
        voiceHuman = policeVS.GetComponent<Human_AI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
