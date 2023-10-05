using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;


//Script developed by chatgpt for base camera switching
public class CameraSwitch : MonoBehaviour
{
    [Header("The two cameras for first and third person need references")]
    public CinemachineFreeLook thirdPersonCamera;
    public CinemachineFreeLook firstPersonCamera;
    [Header("Clue Image Manager should be on your GameManager object-will toggle on UI.")]
    public ClueImageManager clueImageManager; 
    [HideInInspector]
    public bool isFirst;
    private CameraController cameraControls;

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
        isFirst = false;
        clueImageManager.TurnUIOff();
        //enable player rotation
        thirdPersonCamera.gameObject.SetActive(true);
        firstPersonCamera.gameObject.SetActive(false);
    }

    void EnableFirstPersonCamera()
    {
        isFirst = true;
        clueImageManager.TurnUIOn();
        //lock player rotation
        firstPersonCamera.gameObject.SetActive(true);
        thirdPersonCamera.gameObject.SetActive(false);
    }
}