using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDelete : MonoBehaviour
{
    // Start is called before the first frame update
    public void DeleteAll()
    {
        SaveSystem.DeleteNPC();
    }

    
}
