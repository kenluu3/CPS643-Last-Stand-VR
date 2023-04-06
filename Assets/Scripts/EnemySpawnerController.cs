using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{

    public static int totalEnemiesAlive = 0;
    public GameObject[] enemyPrefabs;

    public float spawnInterval;
    public int enemiesPerSpawner;

    private List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        StartNewWave();
    }

    private void Update()
    {
        Debug.Log(totalEnemiesAlive);
        if (totalEnemiesAlive == 0)
        {
            StartNewWave();
        }
    }

    private void StartNewWave()
    {
        Debug.Log("Starting new wave for spawner: " + gameObject.name);
        enemies.Clear();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerSpawner; i++)
        {
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyPrefab = enemyPrefabs[randomEnemyIndex];
            Vector3 enemySpawnPosition;
            enemySpawnPosition = new Vector3(transform.position.x, enemyPrefab.transform.position.y, transform.position.z);
            GameObject enemy = Instantiate(enemyPrefab, enemySpawnPosition, transform.rotation);
            enemies.Add(enemy);
            totalEnemiesAlive++;

            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.spawner = this;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        totalEnemiesAlive--;
        Debug.Log(totalEnemiesAlive);
    }
}