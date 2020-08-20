using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{


    public GameObject Enemy;
    public int xPos;
    public int zPos;
    public int enemyCount;
    //initialized in unity
    public float spawnDelay;
    public int maxEnemys;

    void Start()
    {
        StartCoroutine(EnemySpawnner());
    }


    // find number of enemys spawned and increase by one every set number of seconds until max number of enemys are spawned
    IEnumerator EnemySpawnner()
    {
        while (enemyCount < maxEnemys)
        {
            xPos = Random.Range(-10, 10);
            zPos = Random.Range(-10, 10);
            Instantiate(Enemy, new Vector3(xPos, -1.13f, zPos), Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
            enemyCount += 1;
        }
    }
}