using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public float playerX;
    public float playerY;
    public float playerZ;

    public PlayerData(Transform playerTransform)
    {
        playerX = playerTransform.position.x;
        playerY = playerTransform.position.y;
        playerZ = playerTransform.position.z;
    }

}
