using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
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
        if (spawnPoints.Length == 0 || EnemyPool.Instance == null) return;

        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = EnemyPool.Instance.GetEnemy();

        if (enemy != null)
        {
            enemy.transform.position = randomSpawnPoint.position;
            enemy.transform.rotation = randomSpawnPoint.rotation;
            
            EnemyHealth healthComponent = enemy.GetComponent<EnemyHealth>();
            if (healthComponent != null)
            {
                healthComponent.ResetHealth();
            }

            ShieldDecorator existingShield = enemy.GetComponent<ShieldDecorator>();
            if (existingShield != null)
            {
                Destroy(existingShield);
            }

            if (Random.value > 0.7f)
            {
                ShieldDecorator shield = enemy.AddComponent<ShieldDecorator>();
                shield.SetupDecorator(healthComponent);
                shield.ResetShield(10);
            }

            Enemy enemyComponent = enemy.GetComponent<Enemy>();
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

            enemy.SetActive(true);
        }
    }
}