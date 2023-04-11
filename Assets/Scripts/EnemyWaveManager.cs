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

    public static bool allSpawnersRegistered = false;

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

    public void ResetSpawner()
    {
        numActiveSpawners--;
        if (numActiveSpawners == 0)
        {
            StartNewWave();
            numActiveSpawners = totalNumberOfSpawners;
        }
    }

    public void StartNewWave()
    {
        if (allSpawnersRegistered)
        {
            foreach (EnemySpawnerController spawner in spawners)
            {
                spawner.StartNewWave(enemyPrefabs, enemiesPerSpawner);
            }
        }
    }

    private void OnDisable()
    {
        spawners.Clear();
        allSpawnersRegistered = false;
        numActiveSpawners = 0;
    }
}
