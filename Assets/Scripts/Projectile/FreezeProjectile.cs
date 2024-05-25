using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeProjectile : MonoBehaviour
{
    [Header("Freeze Settings")]
    [SerializeField] public float freezeDuration = 3f;
    [SerializeField] private LayerMask enemyLayer;

    private bool hasFrozen = false;

    private PlayerProjectile projectile;
    private CharacterAttack characterAttack;

    private void Awake()
    {
        projectile = GetComponent<PlayerProjectile>();
        characterAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAttack>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasFrozen && collision.CompareTag("Enemy"))
        {
            if (characterAttack.canFreeze)
            {
                Freeze(collision);
            }
        }
    }

    public void Freeze(Collider2D enemy)
    {
        hasFrozen = true;

        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
           StopAllCoroutines();
           enemyMovement.StartCoroutine(enemyMovement.DisableMovementForDuration(1f));
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

    public  void DisableProjectile()
    {
        hasFrozen = false;
    }
}
