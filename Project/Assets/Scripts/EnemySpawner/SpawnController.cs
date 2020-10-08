using System.Collections;
using TMPro;
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

    #region Text messages

    public TextMeshProUGUI waitingWaveStartText;
    public TextMeshProUGUI waveState;

    #endregion
    
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
        waveState.enabled = true;
        waveState.text = $"Wave {actualWave + 1} starting";
        yield return new WaitForSeconds(2f);
        waveState.enabled = false;
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
        
        waveState.enabled = enabled;
        waveState.text = "Wave finished";

        if (actualWave < waveList.Length)
        {
            StartCoroutine(StartingWave());
            yield return new WaitForSeconds(6f);
            StartCoroutine(StartWave());
            if (actualWave % 3 == 0)
            {
                difficultyRate++;
            }

            waveState.text = $"Wave {actualWave + 1}";
            yield return new WaitForSeconds(2);
            waveState.enabled = false;
        }
        else
        {
            waveState.text = "Stage finished!";
            waveState.enabled = false;
        }
    }

    IEnumerator StartingWave()
    {
        waitingWaveStartText.enabled = true;
        for (int i = 5; i >= 0; i--)
        {
            waitingWaveStartText.text = "Time left: "+i;
            yield return new WaitForSeconds(1);
        }
        waitingWaveStartText.enabled = false;
    }
    
}