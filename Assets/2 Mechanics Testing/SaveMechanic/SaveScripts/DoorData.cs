using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//chat gpt helped quick write the script given pseudocode 
[System.Serializable]
public class DoorData
{
    public bool[] boolList;

    public DoorData(GameObject[] doors)
    {
        // Ensure doors array is not null
        if (doors == null)
        {
            Debug.LogError("Doors array is null.");
            return;
        }

        // Clear the existing boolList
        boolList = new bool[doors.Length];

        // Check each object in the input array
        for (int i = 0; i < doors.Length; i++)
        {
            // Check if the object is active
            bool isActive = doors[i] != null && doors[i].activeSelf;

            // Assign the result to the boolList
            boolList[i] = isActive;
        }
    }

}