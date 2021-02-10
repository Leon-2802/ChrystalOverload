using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform target;
    public GameObject enemyPrefab;
    public float respawnTime;
    public float respawnTime2;

    public void Start()
    {
        StartCoroutine(spawnTimer());
        StartCoroutine(spawnTimer2());
    }

    private void spawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab) as GameObject;
        enemy.transform.position = new Vector2 (9, 1);
    }
    private IEnumerator spawnTimer()
    {
        yield return new WaitForSeconds(respawnTime);
        spawnEnemy();
    }

    private IEnumerator spawnTimer2()
    {
        yield return new WaitForSeconds(respawnTime2);
        spawnEnemy();
    }

}
