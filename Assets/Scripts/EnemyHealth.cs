using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    public static event Action OnEnemyDeath;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            OnEnemyDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}