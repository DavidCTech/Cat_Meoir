using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class VS_Apartment_PP_Blend : MonoBehaviour
{

    [Header("Post Processing Volumes and Gameobjects")]
    public Volume hallwayVolume;
    public Volume ownerRoomVolume, emptyRoomVolume;
    private bool hitCollider;
    public GameObject roomHallway, roomEmpty, roomOwner;

    void Start()
    {
        if (roomHallway != null)
        {
            roomHallway.gameObject.SetActive(false);
        }

        hallwayVolume.weight = 0;
        ownerRoomVolume.weight = 1;
        emptyRoomVolume.weight = 1;
    }

    void Update()
    {
        CheckHallway();
        CheckBothRooms();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!hitCollider)
            {
                Debug.Log("In Hallway");
                hitCollider = true;
                roomHallway.gameObject.SetActive(true);
                roomEmpty.gameObject.SetActive(false);
                roomOwner.gameObject.SetActive(false);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Not In Hallway");
            hitCollider = false;
            roomHallway.gameObject.SetActive(false);
            roomEmpty.gameObject.SetActive(true);
            roomOwner.gameObject.SetActive(true);
        }
    }

    public void CheckHallway()
    {
        if (roomHallway.gameObject.activeInHierarchy && hallwayVolume.weight < 1)
        {
            hallwayVolume.weight += Time.deltaTime;

            if (hallwayVolume.weight >= 1)
            {
                hallwayVolume.weight = 1;
            }
        }
        if (!roomHallway.gameObject.activeInHierarchy && hallwayVolume.weight > 0)
        {
            hallwayVolume.weight -= Time.deltaTime;

            if (hallwayVolume.weight <= 0)
            {
                hallwayVolume.weight = 0;
                hallwayVolume.gameObject.SetActive(false);
            }
        }
    }

    public void CheckBothRooms()
    {
        if (roomEmpty.gameObject.activeInHierarchy && emptyRoomVolume.weight < 1)
        {
            emptyRoomVolume.weight += Time.deltaTime;
            ownerRoomVolume.weight += Time.deltaTime;

            if (emptyRoomVolume.weight >= 1)
            {
                emptyRoomVolume.weight = 1;
                ownerRoomVolume.weight = 1;

            }
        }
        if (!roomEmpty.gameObject.activeInHierarchy && emptyRoomVolume.weight > 0)
        {
            emptyRoomVolume.weight -= Time.deltaTime;
            ownerRoomVolume.weight -= Time.deltaTime;

            if (emptyRoomVolume.weight <= 0)
            {
                emptyRoomVolume.weight = 0;
                ownerRoomVolume.weight = 0;

            }
        }
    }
}
