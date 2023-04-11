using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public float spawnInterval;
    public int enemiesPerSpawner;

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
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
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
