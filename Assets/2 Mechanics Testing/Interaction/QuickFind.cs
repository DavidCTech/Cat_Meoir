using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickFind : MonoBehaviour
{
    public List<GameObject> objects;
    public string searchTag; 
    // Start is called before the first frame update
    void Start()
    {

        /*
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag(searchTag))
            {
                objects.Add(obj);
                // Found object with the desired tag
                Debug.Log("Found object with tag " + searchTag + ": " + obj.name);
            }
        }

        Invoke("FindAgain", 5f);
       */

        /*
        AudioSource[] foundListeners = FindObjectsOfType<AudioSource>();

        foreach (AudioSource listener in foundListeners)
        {
            objects.Add(listener.gameObject);

            // Optional: Print the names of the found objects
            Debug.Log("Found object with AudioListener: " + listener.gameObject.name);
        }
        */

        /*
        Description[] foundDescriptions = FindObjectsOfType<Description>();

        foreach (Description description in foundDescriptions)
        {
            objects.Add(description.gameObject);

            // Optional: Print the names of the found objects
            Debug.Log("Found object with AudioListener: " + description.gameObject.name);
        }
        */
        string layerName = searchTag;
        int layerIndex = LayerMask.NameToLayer(layerName);

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == layerIndex)
            {
                objects.Add(obj);
                // Found object with the desired layer
                Debug.Log("Found object with layer " + layerName + ": " + obj.name);
            }
        }

    }
    void FindAgain()
    {
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag(searchTag);

        //GameObject[] foundObjects = FindObjectOfType<AudioListener>().gameObject;
        objects.AddRange(foundObjects);
        Debug.Log("Tag search: " + searchTag);
        // Optional: Print the names of the found objects
        foreach (GameObject obj in foundObjects)
        {
            objects.Add(obj);
            Debug.Log("Found object with tag " + searchTag + ": " + obj.name);
        }
    }

}
