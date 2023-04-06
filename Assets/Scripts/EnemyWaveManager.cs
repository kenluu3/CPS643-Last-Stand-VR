using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager instance;
    public GameObject[] enemyPrefabs;
    public int enemiesPerSpawner;
    public int totalNumberOfSpawners;

    private List<EnemySpawnerController> spawners = new List<EnemySpawnerController>();
    private int numActiveSpawners = 0;

    private bool allSpawnersRegistered = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (allSpawnersRegistered)
        {
            StartNewWave();
        }
    }

    public void RegisterSpawner(EnemySpawnerController spawner)
    {
        spawners.Add(spawner);
        numActiveSpawners++;

        if (numActiveSpawners == totalNumberOfSpawners)
        {
            allSpawnersRegistered = true;
            StartNewWave();
        }
    }

    public void UnregisterSpawner()
    {
        numActiveSpawners--;
        if (numActiveSpawners == 0)
        {
            StartNewWave();
        }
    }

    public void StartNewWave()
    {
        foreach (EnemySpawnerController spawner in spawners)
        {
            spawner.StartNewWave(enemyPrefabs, enemiesPerSpawner);
        }
    }
}
