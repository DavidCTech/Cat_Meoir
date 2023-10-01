using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSnap : MonoBehaviour
{
    //This Script goes on the camera. 

    //Attach the first person transform and the camera turn script 
    public Transform firstPersonTransform;
    public CameraTurn cameraTurn;

    //original transform info 
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalLocalScale;

    //first person transform info 
    private Vector3 firstPosition;
    private Quaternion firstRotation;
    private Vector3 firstLocalScale;

   
    void Awake()
    {
        if (!cameraTurn)
        {
            Debug.Log("Put the Camera Turn onto the camera");
        }
        //assigning first person camera transform information 
        firstPosition = firstPersonTransform.localPosition;
        firstRotation = firstPersonTransform.localRotation;
        firstLocalScale = firstPersonTransform.localScale;

        //assigning original transform information 
        originalPosition = this.gameObject.transform.localPosition;
        originalRotation = this.gameObject.transform.localRotation;
        originalLocalScale = this.gameObject.transform.localScale;

    }

    void ToFirstPerson()
    {
        cameraTurn.canTurn = true; 
        this.gameObject.transform.localPosition = firstPosition;
        this.gameObject.transform.localRotation = firstRotation;
        this.gameObject.transform.localScale = firstLocalScale;

    }

    void ToThirdPerson()
    {
        cameraTurn.canTurn = false;
        //if key E up, transform back to original position
        this.gameObject.transform.localPosition = originalPosition;
        this.gameObject.transform.localRotation = originalRotation;
        this.gameObject.transform.localScale = originalLocalScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToFirstPerson(); 
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            ToThirdPerson(); 
        }
    }
}
