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
    [Header("Need a reference to the UI that shows the last picture taken.")]
    public Image lastPhotoImage;
    [Header("Reference to the animator for the UI.")]
    public Animator photoAnim;
    [Header("Reference to the scene name info object that it uses to locate the clues.")]
    public SceneInfo sceneInfo;
    [Header("Reference to the sound script for picture taking. ")]
    public PlaySound playSound;
    public AudioClip audioClip;
    [Header("Reference to the light game Object for the picture. ")]
    public GameObject light;
    [Header("The time it takes to make the picture ")]
    public float delayTime;
    [Header("Delay of taking picture after pressing button( no delay feels good) ")]
    public float delaySnap;
    [Header("This is the clue UI main popup gameobj")]
    public GameObject popUp;
    [Header("This is the clue UI optional popup gameobj")]
    public GameObject optionalPopUp;


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
    private bool isClue; //because bools can not be null
    private bool passMainBool;
    private bool isTakingPicture;

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

        var rebindsCam = PlayerPrefs.GetString("rebindsCam");
        if (!string.IsNullOrEmpty(rebindsCam))
            cameraControls.asset.LoadBindingOverridesFromJson(rebindsCam);

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
        if (!isTakingPicture)
        {
            if (captureCamera != null && renderTexture != null)
            {
                if (cameraSwitch.isFirst)
                {
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        captureCamera.targetTexture = renderTexture;
                        captureCamera.Render();


                        AudioSnap();
                        LightSnap();
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

    }
    //this method assigns a picture to the last photo taken placeholder
    private void AssignPicture(Sprite passedImage)
    {
        lastPhotoImage.sprite = passedImage;

    }

    //checks if the object is a clue ( Chat GPT Helped fix scripting issues here )
    private bool checkObject()
    {
        isClue = false;
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
                        if (target.gameObject.GetComponent<Description>() != null)
                        {
                            passDescription = target.gameObject.GetComponent<Description>().description;
                        }
                        if (target.gameObject.GetComponent<MainBool>() != null)
                        {
                            isClue = true;
                            passMainBool = target.gameObject.GetComponent<MainBool>().isMainClue;
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



    private void AudioSnap()
    {
        //making sound 


        if (playSound != null && audioClip != null)
        {
            playSound.PutInClip(audioClip);
        }

    }
    private void LightSnap()
    {
        //this should also flash a light so insantiate the light and dim it coroutine - set active then have it have a timer to make it inactive on it with the routine 
        // put the render texture in a coroutine or something 
        if (FindAnyObjectByType<AccessibilityManager>() != null)
        {
            if (!AccessibilityManager.instance.cameraFlashToggle.isOn)
            {
                if (light != null)
                {
                    light.SetActive(true);

                }
            }
        }
    }

    public IEnumerator TakeSnapshot(RenderTexture renderTexture)
    {
        isTakingPicture = true;
        yield return new WaitForSeconds(delaySnap);

        captureCamera.targetTexture = renderTexture;


        //ui texture stuff 

        captureCamera.Render();

        Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA64, false);
        //try to figure out the post processing done 
        RenderTexture.active = renderTexture;

        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();
        RenderTexture.active = null;
        captureCamera.targetTexture = null;





        //passing info 
        passSprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        //assigning the sprite to the texture 
        AssignPicture(passSprite);


        passTexture = texture2D;
        passSceneName = "";
        if (sceneInfo != null)
        {
            passSceneName = sceneInfo.sceneInfo;
        }


        passDescription = "";

        passString = null;
        passBool = checkObject();




        Debug.Log(passMainBool);
        if (isClue)
        {
            if (passMainBool == true)
            {
                //turn clue found ui on 
                popUp.SetActive(true);
                //turn off clue found ui
                Invoke("ClueFoundUIOff", delayTime);
            }

            if (passMainBool == false)
            {
                optionalPopUp.SetActive(true);
            }
        }

        //animate the camera picture 
        if (photoAnim != null)
        {
            photoAnim.enabled = true;
            photoAnim.Play("PhotoBlack", -1, 0);

        }


        //photoManager.addPictureToList(passSprite, passBool, passString, passRender);
        photoManager.addPictureToList(passSprite, passBool, passString, passTexture, passDescription, passSceneName, passMainBool);
        //save this data 
        photoManager.GetComponent<ClueSaves>().SaveClues();
        Invoke("OutCoroutine", delayTime);

    }


    private void ClueFoundUIOff()
    {
        popUp.SetActive(false);
    }
    private void OutCoroutine()
    {
        isTakingPicture = false;
    }

    public void ActionsResetAndLoad()
    {
        //takes off the subscription to prevent memory leaks 
        cameraControls.Camera.CatMemorySnap.performed -= OnCatMemorySnap;
        cameraControls.Camera.CatMemorySnap.Disable();

        cameraControls = new CameraController();

        var rebindsCam = PlayerPrefs.GetString("rebindsCam");
        if (!string.IsNullOrEmpty(rebindsCam))
            cameraControls.asset.LoadBindingOverridesFromJson(rebindsCam);

        cameraControls.Camera.CatMemorySnap.performed += OnCatMemorySnap;
        cameraControls.Camera.CatMemorySnap.Enable();
    }
}