using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public float spawnInterval;
    public int enemiesPerSpawner;
    public static int enemiesKilled;

    private List<GameObject> enemies = new List<GameObject>();

    public void StartNewWave(GameObject[] enemyPrefabs, int enemiesPerSpawner)
    {
        enemies.Clear();
        StartCoroutine(SpawnEnemies(enemyPrefabs, enemiesPerSpawner));
    }

    private IEnumerator SpawnEnemies(GameObject[] enemyPrefabs, int enemiesPerSpawner)
    {
        for (int i = 0; i < enemiesPerSpawner; i++)
        {
            // 2 index: adam, 1 index: flying, 0 index: mech
            float randomIndexTemp = Random.Range(0f, (float)enemyPrefabs.Length);
            int randomEnemyIndex;
            if (randomIndexTemp >= 1.0)
            {
                randomEnemyIndex = 2;
            }
            else if (randomIndexTemp > 0.5)
            {
                randomEnemyIndex = 1;
            }
            else
            {
                randomEnemyIndex = 0;
            }

            GameObject enemyPrefab = enemyPrefabs[randomEnemyIndex];
            Vector3 enemySpawnPosition;
            enemySpawnPosition = new Vector3(transform.position.x, enemyPrefab.transform.position.y, transform.position.z);
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = enemySpawnPosition;
            enemy.tag = "Clone";
            enemies.Add(enemy);

            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.spawner = this;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        enemiesKilled++;
        if (enemies.Count == 0 && EnemyWaveManager.allSpawnersRegistered)
        {
            EnemyWaveManager.instance.ResetSpawner();
        }
    }

    private void OnEnable()
    {
        EnemyWaveManager.instance.RegisterSpawner(this);
    }
}
