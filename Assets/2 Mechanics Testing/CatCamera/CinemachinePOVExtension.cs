using UnityEngine;
using Cinemachine;

//based on first person pov cinemachine tutorial by samyam
public class CinemachinePOVExtension : CinemachineExtension
{
    
    public float clampAngleUp = 45f;
    public float clampAngleSide = 90f; 
    public float horizontalSpeed = 30f;
    public float verticalSpeed = 30f; 

    private InputManager inputManager;
    private Vector3 startingRotation;
    private Vector3 updatingRotation; 

 

    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        base.Awake(); 
    }


    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam != null && vcam.Follow != null)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null)
                {
                    startingRotation = transform.localRotation.eulerAngles;
                }
                if( inputManager != null)
                {
                    
                    Vector2 deltaInput = inputManager.GetMouseDelta();

                    updatingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                    updatingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;

                    //Debug.Log("Updating rotationX: " + updatingRotation.x + "Updating RotationY: " + updatingRotation.y);
                    updatingRotation.y = Mathf.Clamp(updatingRotation.y, startingRotation.x-clampAngleUp, startingRotation.x + clampAngleUp);
                    updatingRotation.x = Mathf.Clamp(updatingRotation.x, startingRotation.y-clampAngleSide, startingRotation.y + clampAngleSide);
                    // ^^^

                    state.RawOrientation = Quaternion.Euler(-updatingRotation.y, updatingRotation.x, 0f);


                }


            }


        }

    }
    public void ResetStart(Quaternion followedRotation)
    {
        startingRotation = followedRotation.eulerAngles;
       // Debug.Log("Starting rotation: " + startingRotation);
    }
    
}
