using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private int damage = 5;
    private Transform target;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        float distanceThisFrame = speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        IDamageable damageable = target.GetComponent<EnemyHealthDecorator>();
        if (damageable == null)
        {
            damageable = target.GetComponent<IDamageable>();
        }

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

        gameObject.SetActive(false);
    }
}