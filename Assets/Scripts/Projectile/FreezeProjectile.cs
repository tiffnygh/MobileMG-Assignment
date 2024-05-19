using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeProjectile : PlayerProjectile
{
    [Header("Freeze Settings")]
    [SerializeField] public float freezeDuration = 3f;
    [SerializeField] private LayerMask enemyLayer;

    private bool hasFrozen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasFrozen && collision.CompareTag("Enemy"))
        {
            Freeze(collision);
        }
    }

    private void Freeze(Collider2D enemy)
    {
        hasFrozen = true;

        // Disable the enemy's movement component
        if (enemy.CompareTag("Enemy") && enemy.gameObject.layer != 9)
        {
            enemy.GetComponent<Health>().TakeDamage(Damage);
        }
        else
        {
            return;
        }

        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            StartCoroutine(DisableMovementForDuration(enemyMovement, freezeDuration));
        }

        // Optionally, play freeze animation/sound here

        // Return projectile to pool
        DisableProjectile();
    }

    private IEnumerator DisableMovementForDuration(EnemyMovement enemyMovement, float duration)
    {
        enemyMovement.enabled = false;
        Debug.Log("DISABLE");
        yield return new WaitForSeconds(duration);
        enemyMovement.enabled = true;
        Debug.Log("ENABLE");

    }

    public override void DisableProjectile()
    {
        base.DisableProjectile();
        hasFrozen = false;
    }
}
