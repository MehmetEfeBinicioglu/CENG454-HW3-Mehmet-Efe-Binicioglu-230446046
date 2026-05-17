using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private Transform[] spawnPoints;

    private float nextSpawnTime;

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoints.Length == 0) return;

        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject spawnedEnemy = Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

        Enemy enemyComponent = spawnedEnemy.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            if (Random.value > 0.5f)
            {
                enemyComponent.SetMovementStrategy(new DirectMoveStrategy());
            }
            else
            {
                enemyComponent.SetMovementStrategy(new FastZigZagMoveStrategy());
            }
        }
    }
}