using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraTakePicture : MonoBehaviour
{
    //this script goes on the camera 
    [Header("Reference to a RenderTexture- any not in use in asset folder.")]
    public RenderTexture renderTexture; // Reference to the Render Texture.

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
    private Camera captureCamera; // Reference to the camera you want to capture.


    private void Start()
    {
        captureCamera = this.gameObject.GetComponent<Camera>();
    }

    //checks if the object is a clue 
    private bool checkObject()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(this.gameObject.transform.position, fovRadius, clueMask);
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - this.gameObject.transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < fovAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(this.gameObject.transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    passString = target.gameObject.name;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    //update eeeee
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
           
            if (captureCamera != null && renderTexture != null)
            {
                // Set the camera's target texture to the Render Texture.
                captureCamera.targetTexture = renderTexture;
                captureCamera.Render();
                StartCoroutine(TakeSnapshot(renderTexture));

                captureCamera.targetTexture = null;
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