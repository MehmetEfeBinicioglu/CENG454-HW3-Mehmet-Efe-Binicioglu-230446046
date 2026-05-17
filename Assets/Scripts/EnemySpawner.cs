using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int maxWaves = 3;
    [SerializeField] private int enemiesPerWave = 5;

    private float timer;
    private int currentWave = 0;
    private int enemiesSpawnedInCurrentWave = 0;
    private bool isSpawningFinished = false;

    private void Update()
    {
        if (isSpawningFinished)
        {
            CheckWinCondition();
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        GameObject enemy = EnemyPool.Instance.GetPooledEnemy();
        if (enemy != null)
        {
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            enemy.transform.position = randomPoint.position;
            enemy.transform.rotation = randomPoint.rotation;
            enemy.SetActive(true);

            enemiesSpawnedInCurrentWave++;

            if (enemiesSpawnedInCurrentWave >= enemiesPerWave)
            {
                enemiesSpawnedInCurrentWave = 0;
                currentWave++;

                if (currentWave >= maxWaves)
                {
                    isSpawningFinished = true;
                }
            }
        }
    }

    private void CheckWinCondition()
    {
        GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy.activeInHierarchy) return;
        }

        GameManager.Instance.WinGame();
    }
}