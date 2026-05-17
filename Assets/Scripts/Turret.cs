using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float range = 10f;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private Transform[] firePoints;

    private Transform target;
    private float fireCountdown = 0f;
    private int currentFirePointIndex = 0;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            if (!enemy.activeInHierarchy) continue;

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null || Time.timeScale == 0f) return;

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        if (ProjectilePool.Instance == null || firePoints.Length == 0) return;

        Transform activeFirePoint = firePoints[currentFirePointIndex];

        GameObject projGO = ProjectilePool.Instance.GetProjectile();
        if (projGO != null)
        {
            projGO.transform.position = activeFirePoint.position;
            projGO.transform.rotation = activeFirePoint.rotation;
            projGO.SetActive(true);

            Projectile projectile = projGO.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.Seek(target);
            }
        }

        currentFirePointIndex = (currentFirePointIndex + 1) % firePoints.Length;
    }
}