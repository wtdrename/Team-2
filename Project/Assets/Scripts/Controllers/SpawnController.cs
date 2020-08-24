using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    public int enemyCount;
    //initialized in unity
    public float spawnDelay;
    public int maxEnemies;
    private int waveCount;
    public GameObject[] enemies;
    public GameObject[] spawnPoints;
    

    void Start()
    {
        if (enemies != null)
        {
            StartCoroutine(EnemySpawnner());
            waveCount = 1;
        }


    }
   


    // find number of enemys spawned and increase by one every set number of seconds until max number of enemys are spawned
    IEnumerator EnemySpawnner()
    {
        while (enemyCount < maxEnemies)
        {
            Vector3 spawnpoint = spawnPoints[Random.Range(0,2)].transform.position;
            Instantiate(enemies[Random.Range(0,9)], spawnpoint, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
            enemyCount += 1;
        }
    }

    private void Update()
    {
        
    }
}