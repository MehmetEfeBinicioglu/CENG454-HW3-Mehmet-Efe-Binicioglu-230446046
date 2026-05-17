using UnityEngine;

public class FastZigZagMoveStrategy : IMovementStrategy
{
    private float frequency = 5f;
    private float magnitude = 2f;

    public void Move(Transform enemyTransform, Transform targetTransform, float speed)
    {
        if (targetTransform == null) return;

        Vector3 direction = (targetTransform.position - enemyTransform.position).normalized;
        Vector3 right = Vector3.Cross(direction, Vector3.up).normalized;

        Vector3 forwardMovement = direction * speed * Time.deltaTime;
        Vector3 sideMovement = right * Mathf.Sin(Time.time * frequency) * magnitude * Time.deltaTime;

        enemyTransform.position += forwardMovement + sideMovement;
    }
}