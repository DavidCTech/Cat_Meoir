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

                    startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                    startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                    startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngleUp, clampAngleUp);
                    startingRotation.x = Mathf.Clamp(startingRotation.x, -clampAngleSide, clampAngleSide);


                    state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);


                }


            }
        }

    }
}
