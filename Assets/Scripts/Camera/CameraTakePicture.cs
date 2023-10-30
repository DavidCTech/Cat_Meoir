using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.SceneManagement;

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
    [Header("Need a reference to the UI image that shows the last picture taken.")]
    public Image lastPhotoImage;
    [Header("Reference to the scene name info object that it uses to locate the clues.")]
    public GameObject sceneName;


    private bool passBool;
    private Sprite passSprite;
    private string passString;
    private Camera captureCamera;
    private int locationName = 0; //int that will be the name string for the non clue images in order to organize them 
    private CameraSwitch cameraSwitch;
    private CameraController cameraControls;
    private int i = 0; 
    private string saveFolder = "CatMeoirSavedImages";
    private Texture2D passTexture;
    private string passDescription;
    private string passSceneName; 

    private void Awake()
    {
        captureCamera = this.gameObject.GetComponent<Camera>();
        cameraSwitch = this.gameObject.GetComponent<CameraSwitch>();
        cameraControls = new CameraController();
    }
    private void OnEnable()
    {
        if (cameraControls == null)
        {
            cameraControls = new CameraController();

        }

        // makes a subscription to the catmemory zoom 
        cameraControls.Camera.CatMemorySnap.performed += OnCatMemorySnap;
        cameraControls.Camera.CatMemorySnap.Enable();

    }
    private void OnDisable()
    {
        //takes off the subscription to prevent memory leaks 
        cameraControls.Camera.CatMemorySnap.performed -= OnCatMemorySnap;
        cameraControls.Camera.CatMemorySnap.Disable();
    }


    private void OnCatMemorySnap(InputAction.CallbackContext context)
    {
        // Set the camera's target texture to the Render Texture.
        //check if the camera is in first person 
        if (captureCamera != null && renderTexture != null)
        {
            if (cameraSwitch.isFirst)
            {
               if(!EventSystem.current.IsPointerOverGameObject())
               {
                    captureCamera.targetTexture = renderTexture;
                    captureCamera.Render();
                    StartCoroutine(TakeSnapshot(renderTexture));
                    captureCamera.targetTexture = null;

               }

            }

        }
        else
        {
            Debug.LogError("Script not on Camera or Render Texture not assigned!");
        }

    }
    //this method assigns a picture to the last photo taken placeholder
    private void AssignPicture(Sprite passedImage) 
    { 
 
        lastPhotoImage.sprite = passedImage;
   
    }

    //checks if the object is a clue ( Chat GPT Helped fix scripting issues here )
    private bool checkObject()
    {
        passString = locationName.ToString();
        locationName++; 
        Collider[] rangeChecks = Physics.OverlapSphere(this.gameObject.transform.position, fovRadius, clueMask);

        if (rangeChecks.Length != 0)
        {
            bool anyObjectInFOV = false; 

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
                        if(target.gameObject.GetComponent<Description>() != null)
                        {
                            passDescription = target.gameObject.GetComponent<Description>().description;
                        }
                        if (target.gameObject.GetComponent<CutSceneClue>() != null)
                        {
                            target.gameObject.GetComponent<CutSceneClue>().CutScenePlay(); 
                        }

                        anyObjectInFOV = true;  
                                               
                    }
                }
            }

            return anyObjectInFOV;  // Return outside the loop
        }
        else
        {
            return false;
        }
    }




    
    public IEnumerator TakeSnapshot(RenderTexture renderTexture)
    {
        yield return new WaitForEndOfFrame();
        captureCamera.targetTexture = renderTexture;
        captureCamera.Render();

        Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();
        RenderTexture.active = null;
        captureCamera.targetTexture = null;

        passSprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
        passTexture = texture2D;
        passSceneName = "";
        if (sceneName != null)
        {
            passSceneName = sceneName.GetComponent<SceneInfo>().sceneInfo; 
        }
        

        passDescription = "";
        passString = null;
        passBool = checkObject();
        AssignPicture(passSprite);
        //photoManager.addPictureToList(passSprite, passBool, passString, passRender);
        photoManager.addPictureToList(passSprite, passBool, passString, passTexture, passDescription, passSceneName);

    }

}