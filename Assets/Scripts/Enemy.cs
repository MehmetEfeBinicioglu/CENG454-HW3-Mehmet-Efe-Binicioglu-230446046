using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 1.5f;

    private Transform targetCore;
    private float nextAttackTime;
    private IMovementStrategy movementStrategy;

    private void Start()
    {
        CoreHealth core = FindFirstObjectByType<CoreHealth>();
        if (core != null)
        {
            targetCore = core.transform;
        }

        if (movementStrategy == null)
        {
            movementStrategy = new DirectMoveStrategy();
        }
    }

    public void SetMovementStrategy(IMovementStrategy strategy)
    {
        movementStrategy = strategy;
    }

    private void Update()
    {
        if (targetCore == null || Time.timeScale == 0f) return;

        float distance = Vector3.Distance(transform.position, targetCore.position);

        if (distance > attackRange)
        {
            movementStrategy.Move(transform, targetCore, speed);
        }
        else
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        if (targetCore == null) return;

        IDamageable damageable = targetCore.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount);
        }

        nextAttackTime = Time.time + attackCooldown;
    }
}