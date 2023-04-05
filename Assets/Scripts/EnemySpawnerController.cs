using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public float spawnInterval;
    public int enemiesPerWave;

    private static int enemiesSpawned = 0;
    private static bool isSpawning = false;
    private float spawnTimer = 0f;

    private void Start()
    {
        if (!isSpawning && enemiesSpawned == 0)
        {
            StartNewWave();
        }
    }

    private void Update()
    {
        if (!isSpawning && enemiesSpawned == 0)
        {
            StartNewWave();
        }

        spawnTimer += Time.deltaTime;

        if (isSpawning && spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    private void StartNewWave()
    {
        isSpawning = true;
        enemiesSpawned = 0;
    }

    private void SpawnEnemy()
    {
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[randomEnemyIndex];
        Vector3 enemySpawnPosition;
        enemySpawnPosition = new Vector3(transform.position.x, enemyPrefab.transform.position.y, transform.position.z);
        GameObject enemy = Instantiate(enemyPrefab, enemySpawnPosition, transform.rotation);

        enemiesSpawned++;
        if (enemiesSpawned >= enemiesPerWave)
        {
            isSpawning = false;
        }
    }
}