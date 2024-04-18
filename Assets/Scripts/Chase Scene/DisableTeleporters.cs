using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTeleporters : MonoBehaviour
{
    public GameObject[] teleporters;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < teleporters.Length; i++)
            {
                teleporters[i].SetActive(false);
            }

            Destroy(this.gameObject);
        }
    }
}
