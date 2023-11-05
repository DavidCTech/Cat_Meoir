using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapControl : MonoBehaviour
{
    public GameObject playerMap;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMapOpenClose()
    {
        if (playerMap != null)
        {
            // Toggle the map UI's visibility
            playerMap.SetActive(!playerMap.activeSelf);
        }
    }
}
