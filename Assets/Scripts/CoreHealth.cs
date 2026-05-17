using System;
using UnityEngine;

public class CoreHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public static event Action<int, int> OnCoreHealthChanged;
    public static event Action OnCoreDestroyed;

    private void Start()
    {
        currentHealth = maxHealth;
        OnCoreHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnCoreHealthChanged?.Invoke(currentHealth, maxHealth);
            OnCoreDestroyed?.Invoke();
        }
        else
        {
            OnCoreHealthChanged?.Invoke(currentHealth, maxHealth);
        }
    }
}