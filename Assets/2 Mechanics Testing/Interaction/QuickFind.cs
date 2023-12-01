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
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag(searchTag);
        objects.AddRange(foundObjects);

        // Optional: Print the names of the found objects
        foreach (GameObject obj in foundObjects)
        {
            Debug.Log("Found object with tag " + searchTag + ": " + obj.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
