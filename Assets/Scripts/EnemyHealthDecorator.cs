using UnityEngine;

public abstract class EnemyHealthDecorator : MonoBehaviour, IDamageable
{
    protected IDamageable wrappedDamageable;

    public virtual void SetupDecorator(IDamageable damageable)
    {
        wrappedDamageable = damageable;
    }

    public abstract void TakeDamage(int amount);
}