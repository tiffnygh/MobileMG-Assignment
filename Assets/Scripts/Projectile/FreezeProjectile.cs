using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeProjectile : MonoBehaviour
{

    [SerializeField] private LayerMask enemyLayer;

    private bool hasFrozen = false;

    private void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasFrozen && collision.CompareTag("Enemy"))
        {
            if (AttackManager.Instance.canFreeze)
            {
                Freeze(collision);
            }
        }
    }

    public void Freeze(Collider2D enemy)
    {
        hasFrozen = true;

        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null && enemy.gameObject.activeSelf)
        {
           StopAllCoroutines();
           enemy.GetComponent<Health>().FreezeColorFeedback();
           enemyMovement.StartCoroutine(enemyMovement.DisableMovementForDuration(AttackManager.Instance.freezeDuration));
        }

        // Optionally, play freeze animation/sound here

        // Return projectile to pool
        DisableProjectile();
    }


    public  void DisableProjectile()
    {
        hasFrozen = false;
    }
}
