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

    void Start()
    {
        EnableThirdPersonCamera();
    }

    void Update()
    {
        //Need to switch to new input system 
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCamera();
        }
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