using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBoss : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject boss;
    // Start is called before the first frame update
    public void spawn()
    {
        GameObject instance = (GameObject)Instantiate(boss, spawnPoint.position, Quaternion.identity);  
        instance.transform.parent = transform;
    }
}
