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

        //GameObject[] foundObjects = GameObject.FindGameObjectsWithTag(searchTag);
        /*
        GameObject[] foundObjects = FindObjectOfType<AudioListener>().gameObject;
        objects.AddRange(foundObjects);

        // Optional: Print the names of the found objects
        foreach (GameObject obj in foundObjects)
        {
            Debug.Log("Found object with tag " + searchTag + ": " + obj.name);
        }
        */
        AudioSource[] foundListeners = FindObjectsOfType<AudioSource>();

        foreach (AudioSource listener in foundListeners)
        {
            objects.Add(listener.gameObject);

            // Optional: Print the names of the found objects
            Debug.Log("Found object with AudioListener: " + listener.gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
