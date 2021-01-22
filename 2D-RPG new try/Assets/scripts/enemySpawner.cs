using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform target;
    public GameObject enemyPrefab;
    public bool spawning = true;
    public float respawnTime;

    void Update()
    {
        if(spawning == true)
        {
           StartCoroutine(spawnTimer());
        }
    }

    private void spawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab) as GameObject;
        enemy.transform.position = new Vector2 (9, 1);
    }
    private IEnumerator spawnTimer()
    {
        spawning = false;
        yield return new WaitForSeconds(respawnTime);
        spawnEnemy();
        spawning = true;
    }

}
