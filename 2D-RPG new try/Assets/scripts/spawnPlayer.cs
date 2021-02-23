using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlayer : MonoBehaviour
{
    public Transform[] playerSpawns;
    public Transform[] camSpawns;
    public GameObject player;
    public Camera cam;
    // Update is called once per frame
    public void spawnPly(int index)
    {
        Instantiate(player, playerSpawns[index].position, Quaternion.identity);
        Camera camInstance = Camera.Instantiate(cam, camSpawns[index].position, Quaternion.identity) as Camera;
        player.GetComponent<newPlyController>().cam = camInstance;
    }
}
