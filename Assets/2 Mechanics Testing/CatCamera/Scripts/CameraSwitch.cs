using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;


//Script developed by chatgpt for base camera switching
public class CameraSwitch : MonoBehaviour
{
    [Header("The two cameras for first and third person need references")]
    public CinemachineFreeLook thirdPersonCamera;
    public CinemachineVirtualCameraBase firstPersonCamera;
    [Header("Need a reference to the playermovement to freeze during switch.")]
    public PlayerMovement playerMovement;
    [Header("Need a reference to the CinemachinePOVExtension on cam first person.")]
    public CinemachinePOVExtension cinemachinePOXExtension;
    [HideInInspector]
    public bool isFirst;
    public CameraController cameraControls;


    private void Awake()
    {
        cameraControls = new CameraController();
    }
    private void OnEnable()
    {
        if (cameraControls == null)
        {
            cameraControls = new CameraController();

        }

        // makes a subscription to the catmemory zoom 
        cameraControls.Camera.CatMemoryZoom.performed += OnCatMemoryZoom;
        cameraControls.Camera.CatMemoryZoom.Enable();

    }
    private void OnDisable()
    {
        //takes off the subscription to prevent memory leaks 
        cameraControls.Camera.CatMemoryZoom.performed -= OnCatMemoryZoom;
        cameraControls.Camera.CatMemoryZoom.Disable();
    }

    void Start()
    {
        EnableThirdPersonCamera();
    }
    private void OnCatMemoryZoom(InputAction.CallbackContext context)
    {
        ToggleCamera();
    }


    void ToggleCamera()
    {
        if (thirdPersonCamera.gameObject.activeSelf)
        {
            EnableFirstPersonCamera();
        }
        else
        {
            EnableThirdPersonCamera();
        }
    }

    void EnableThirdPersonCamera()
    {
        if (playerMovement != null)
        {
            playerMovement.isFrozen = false;
        }
        else
        {
            Debug.Log("You need to attach the playermovement script into the camera switch script. - camera and player objects respectively. ");
        }

        
        isFirst = false;
        thirdPersonCamera.gameObject.SetActive(true);
        firstPersonCamera.gameObject.SetActive(false);
    }

    void EnableFirstPersonCamera()
    {
        
        if (cinemachinePOXExtension != null)
        {
            cinemachinePOXExtension.ResetStart(firstPersonCamera.Follow.rotation); 
        }
        if(playerMovement != null)
        {
            playerMovement.isFrozen = true;
        }
        else
        {
            Debug.Log("You need to attach the playermovement script into the camera switch script. - camera and player objects respectively. ");
        }

        isFirst = true;
        //lock player rotation
        firstPersonCamera.gameObject.SetActive(true);
        thirdPersonCamera.gameObject.SetActive(false);
    }
}