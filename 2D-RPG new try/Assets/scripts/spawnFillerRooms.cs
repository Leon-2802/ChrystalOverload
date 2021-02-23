using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFillerRooms : MonoBehaviour
{
    public LayerMask whatIsRoom;
    public levelGeneration levelGenerationScr;
    public GameObject[] rooms;

    void Update() 
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        if (roomDetection == null && levelGenerationScr.stopGeneration == true) {
            int rand = Random.Range(0, rooms.Length);
            Instantiate(rooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
