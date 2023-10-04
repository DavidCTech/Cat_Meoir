using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraTakePicture : MonoBehaviour
{
    //this script goes on the camera 
    [Header("Reference to a RenderTexture- any not in use in asset folder.")]
    public RenderTexture renderTexture; 

    [Header("Reference to the PhotoManager Script(look on a Photo manager GameObj)")]
    public PhotoManager photoManager; 

    [Header("Camera AI Settings")]
    [Tooltip("The layers it looks for + angle of sight")]
    public float fovRadius;
    public float fovAngle;
    public LayerMask clueMask;
    public LayerMask obstructionMask;

    private bool passBool;
    private Sprite passSprite;
    private string passString;
    private Camera captureCamera;
    private int locationName = 0; //int that will be the name string for the non clue images in order to organize them 
    private CameraSwitch cameraSwitch; 




    private void Start()
    {
        captureCamera = this.gameObject.GetComponent<Camera>();
        cameraSwitch = this.gameObject.GetComponent<CameraSwitch>();
    }

    //checks if the object is a clue ( Chat GPT Helped fix scripting issues here )
    private bool checkObject()
    {
        passString = locationName.ToString();
        locationName++; 
        Collider[] rangeChecks = Physics.OverlapSphere(this.gameObject.transform.position, fovRadius, clueMask);

        if (rangeChecks.Length != 0)
        {
            bool anyObjectInFOV = false;  // Variable to track if any object within FOV meets conditions

            for (int i = 0; i < rangeChecks.Length; i++)
            {
                Transform target = rangeChecks[i].transform;
                Vector3 directionToTarget = (target.position - this.gameObject.transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < fovAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(this.gameObject.transform.position, target.position);
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        locationName--; 
                        passString = target.gameObject.name;
                        anyObjectInFOV = true;  // Set the flag to true if any object meets conditions
                                                // You might want to perform additional logic here if needed
                    }
                }
            }

            return anyObjectInFOV;  // Return outside the loop
        }
        else
        {
            Debug.LogError("CameraTakePicture: Hey buddy you need to assign the clue layer in this script + have some object in that layer too in the scene.");
            return false;
        }
    }

    //update eeeee
    private void Update()
    {
        //change this to the new input system 
        if (Input.GetKeyDown(KeyCode.R))
        {
           
            if (captureCamera != null && renderTexture != null)
            {
                // Set the camera's target texture to the Render Texture.
                //check if the camera is in first person 
                if (cameraSwitch.isFirst)
                {
                    captureCamera.targetTexture = renderTexture;
                    captureCamera.Render();
                    StartCoroutine(TakeSnapshot(renderTexture));

                    captureCamera.targetTexture = null;
                }
                
            }
            else
            {
                Debug.LogError("Script not on Camera or Render Texture not assigned!");
            }
        }
    }

    //this creates the image and gathers information to export into a scriptable object manager (photoManager) 
    public IEnumerator TakeSnapshot(RenderTexture renderTexture)
    {
        yield return new WaitForEndOfFrame();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          

        //turn render texture to 2D texture- texture format, no mipmaps to avoid error
        Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
        Graphics.CopyTexture(renderTexture, texture2D);

        //turn 2dTexture to sprite send info to the PhotoManger
        passSprite = Sprite.Create(texture2D, rect, new Vector2(0.5f, 0.5f));
        passString = null; 
        passBool = checkObject();
        photoManager.addPictureToList(passSprite, passBool, passString);



    }
    
}