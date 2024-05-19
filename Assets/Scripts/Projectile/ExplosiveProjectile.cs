using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : PlayerProjectile
{
    [Header("Explosion Settings")]
    [SerializeField] private float explosionRadius = 5f;
    private int explosionDamage;
    [SerializeField] private LayerMask enemyLayer;

    private bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        explosionDamage = Damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasExploded && collision.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        hasExploded = true;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Health>().TakeDamage(explosionDamage);
        }

        // Optionally, play explosion animation/sound here

        // Return projectile to pool
        DisableProjectile();
    }

    public override void DisableProjectile()
    {
        base.DisableProjectile();
        hasExploded = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
