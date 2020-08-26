using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Wave")]
public class EnemyInfoSO : ScriptableObject
{
    public int enemyCount;
    //initialized in unity
    public float spawnDelay;
    public int maxEnemies;
    public int waveCount;
    public GameObject[] enemies;
    public GameObject[] spawnPoints;
}
