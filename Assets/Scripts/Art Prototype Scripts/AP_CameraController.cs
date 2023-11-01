using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_CameraController : MonoBehaviour
{
    public Transform target;
    public Transform catTarget, humanTarget;
    public bool isCat, isHuman;
    public GameObject shadowModel, killerModel;

    void Awake()
    {
        if (isHuman)
        {
            target.position = humanTarget.position;
            target.rotation = humanTarget.rotation;
            shadowModel.gameObject.SetActive(false);
            killerModel.gameObject.SetActive(true);
        }

        if (isCat)
        {
            target.position = catTarget.position;
            target.rotation = catTarget.rotation;
            shadowModel.gameObject.SetActive(true);
            killerModel.gameObject.SetActive(false);
        }
    }

    void LateUpdate()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
