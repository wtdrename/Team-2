using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class SpawnController : MonoBehaviour
{
    private ObjectPooler objPooler;
    public static SpawnController Instance { private set; get; }

    public EnemySpawnerSO[] waveList;
    public GameObject[] spawnPoints;
    
    private int actualWave = 0;
    [HideInInspector]public int actualEnemiesAlive = 0;
    
    void Start()
    {
        Instance = this;
        
        objPooler = ObjectPooler.Instance;

        waveList[0].enemyCount = 0;
        StartCoroutine(StartWave());
    }


    // Choose which enemy to spawn from the object pool
    private int difficultyRate = 0;
    IEnumerator StartWave()
    {
        EnemySpawnerSO wave = waveList[actualWave];
        actualWave++;
        while (wave.enemyCount < wave.maxEnemies)
        {
            Vector3 spawnpoint = spawnPoints[Random.Range(0,spawnPoints.Length)].transform.position;
            int randomNumber = Random.Range(0, 21);
            randomNumber += difficultyRate;
            if (randomNumber <= 13)
            {
                objPooler.SpawnFromPool("Low", spawnpoint, Quaternion.identity);
            }else if(randomNumber >= 14 && randomNumber <= 18)
            {
                objPooler.SpawnFromPool("Medium", spawnpoint, Quaternion.identity);
            }else
            {
                objPooler.SpawnFromPool("High", spawnpoint, Quaternion.identity);
            }

            yield return new WaitForSeconds(wave.spawnDelay);
            wave.enemyCount += 1;
            actualEnemiesAlive++;
        }
        StartCoroutine(WaitForEndofWave());
    }
    
    //prepare next wave and each 3 waves increase the probability to spawn higher tier enemies
    IEnumerator WaitForEndofWave()
    {
        while (actualEnemiesAlive > 0)
        {
            yield return new WaitForSeconds(1f);
        }

        if (actualWave < waveList.Length - 1)
        {
            StartCoroutine(StartWave());
            if (actualWave % 3 == 0)
            {
                difficultyRate++;
            }
        }

    }
}