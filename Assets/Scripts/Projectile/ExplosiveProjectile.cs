using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{
    [Header("Explosion Settings")]
    [SerializeField] private float explosionRadius = 5f;
    private int explosionDamage;
    [SerializeField] private LayerMask enemyLayer;

    private bool hasExploded = false;

    private PlayerProjectile projectile;
    private CharacterAttack characterAttack;
    private FreezeProjectile freezeProjectile;

    private void Awake()
    {
        projectile = GetComponent<PlayerProjectile>();
        freezeProjectile = GetComponent<FreezeProjectile>();
        characterAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAttack>();
    }

    // Start is called before the first frame update
    void Start()
    {
        explosionDamage = projectile.Damage;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasExploded && collision.CompareTag("Enemy") && collision.gameObject.layer != 9)
        {
            if (AttackManager.Instance.canAOE)
            {
                Explode();
            }
        }
    }

    private void Explode()
    {
        hasExploded = true;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Health>().TakeDamage(explosionDamage);
            if (AttackManager.Instance.canFreeze)
            {
                freezeProjectile.Freeze(enemy);
            }

        }

        // Optionally, play explosion animation/sound here

        // Return projectile to pool
        DisableProjectile();
    }

    public  void DisableProjectile()
    {
        hasExploded = false;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
