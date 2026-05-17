using UnityEngine;

public class ShieldDecorator : EnemyHealthDecorator
{
    [SerializeField] private int shieldHealth = 10;

    public override void TakeDamage(int amount)
    {
        if (shieldHealth > 0)
        {
            shieldHealth -= amount;
            Debug.Log("Shield absorbed damage! Remaining Shield: " + shieldHealth);
            
            if (shieldHealth <= 0)
            {
                Debug.Log("Shield completely broken!");
            }
        }
        else if (wrappedDamageable != null)
        {
            wrappedDamageable.TakeDamage(amount);
        }
    }

    public void ResetShield(int amount)
    {
        shieldHealth = amount;
    }
}