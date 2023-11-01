using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_PlayerController : MonoBehaviour
{
    public static AP_PlayerController instance;

    [Header("Movement Variables")]
    public float moveSpeed;
    private CharacterController charCon;
    private Vector3 moveInput;
    public float maxAngle = 60f;

    [Header("Movement Adjustment Variables")]
    public float sensitivity;
    public bool invertX, invertY;

    [Header("Camera Variables")]
    public Transform camTrans;

    void Awake()
    {
        if (instance != null)
        {
            instance = this;
        }

        charCon = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!AP_Menu.instance.isPaused && Time.time > 1f)
        {
            //Control Player Movement
            //moveInput.x = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
            //moveInput.z = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
            Vector3 verticalMove = transform.forward * Input.GetAxisRaw("Vertical");
            Vector3 horizontalMove = transform.right * Input.GetAxisRaw("Horizontal");

            moveInput = horizontalMove + verticalMove;
            moveInput.Normalize();
            moveInput = moveInput * moveSpeed;

            charCon.Move(moveInput * Time.deltaTime);

            //Control Camera Rotation
            Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Cam_Horizontal"), Input.GetAxisRaw("Cam_Vertical")) * sensitivity;

            //Inverting the Camera
            if (invertX)
            {
                mouseInput.x = -mouseInput.x;
            }

            if (invertY)
            {
                mouseInput.y = -mouseInput.y;
            }

            //Rotating the player and camera
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, mouseInput.x, 0f));
            camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

            //Debug.Log(camTrans.rotation.eulerAngles.x);

            //Clamp the Camera 
            if (camTrans.rotation.eulerAngles.x > maxAngle && camTrans.rotation.eulerAngles.x < 180f)
            {
                camTrans.rotation = Quaternion.Euler(maxAngle, camTrans.rotation.eulerAngles.y, camTrans.rotation.eulerAngles.z);
            }
            else if (camTrans.rotation.eulerAngles.x > 180f && camTrans.rotation.eulerAngles.x < 360f - maxAngle)
            {
                camTrans.rotation = Quaternion.Euler(-maxAngle, camTrans.rotation.eulerAngles.y, camTrans.rotation.eulerAngles.z);
            }
        }
    }
}
