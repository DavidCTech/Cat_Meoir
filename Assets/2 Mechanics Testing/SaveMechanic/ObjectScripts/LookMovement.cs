using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookMovement : MonoBehaviour
{
    public float maxUp;
    public float minUp;
    public float rotationSpeed;
    public GameObject cameraObject;
    private InputManager inputManager;

    void Start()
    {
        inputManager = GetComponentInParent<InputManager>();
    }

    void FixedUpdate()
    {
        float mouseY = inputManager.GetMouseDelta().y;

        Vector3 localMovement = transform.TransformDirection(0, mouseY * rotationSpeed * Time.deltaTime, 0);

        Vector3 newPosition = transform.localPosition + localMovement;

        newPosition.y = Mathf.Clamp(newPosition.y, minUp, maxUp);

        transform.localPosition = newPosition;
    }
}
