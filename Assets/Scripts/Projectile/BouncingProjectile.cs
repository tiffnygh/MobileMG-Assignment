using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingProjectile : PlayerProjectile
{
    [Header("Ricochet Settings")]
    [SerializeField] private int maxRicochets = 3;
    private int currentRicochets = 0;
    private Collider2D lastBarrierHit;

    private void OnEnable()
    {
        currentRicochets = 0;
        lastBarrierHit = null;
    }

    private void FixedUpdate()
    {
        MoveProjectile();
        CheckForCollisionWithBarrier();
    }

    private void CheckForCollisionWithBarrier()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Barrier") && hit != lastBarrierHit && currentRicochets < maxRicochets)
            {
                if (IsProjectileExitingBarrier(hit))
                {
                    ReflectDirection(hit);
                    lastBarrierHit = hit;
                    currentRicochets++;
                }
            }
            else if (hit.CompareTag("Enemy"))
            {
                HandleEnemyHit(hit);
                return;
            }
        }
    }
    private bool IsProjectileExitingBarrier(Collider2D barrier)
    {
        float distanceFromCenter = Vector3.Distance(barrier.transform.position, transform.position);
        CircleCollider2D barrierCollider = barrier as CircleCollider2D;

        if (barrierCollider != null && distanceFromCenter > barrierCollider.radius)
        {
            return true;
        }

        return false;
    }

    private void ReflectDirection(Collider2D barrier)
    {
        Vector2 normal = ((Vector2)transform.position - barrier.ClosestPoint(transform.position)).normalized;
        Direction = Vector2.Reflect(Direction, normal).normalized;
    }

    private void HandleEnemyHit(Collider2D enemy)
    {
        Health enemyHealth = enemy.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(Damage);
        }
        DisableProjectile();
    }
}
