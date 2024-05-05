using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class ReturnToPool : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask objectMask;
    [SerializeField] private float lifeTime = 2f;

    [Header("Effects")]
    [SerializeField] private ParticleSystem impactPS;


    private Projectile projectile;
    private BossProjectile bossProjectile;
    private EnemyProjectile enemyProjectile;


    private void Start()
    {
        projectile = GetComponent<Projectile>();
        bossProjectile = GetComponent<BossProjectile>();
        enemyProjectile = GetComponent<EnemyProjectile>();
    }

    // Returns this object to the pool
    private void Return()
    {
        if (projectile != null)
        {
            projectile.ResetProjectile();
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (MyLibrary.CheckLayer(other.gameObject.layer, objectMask))
        {
            if (enemyProjectile != null)
            {
                enemyProjectile.DisableEnemyProjectile();
            }

            if (projectile != null)
            {
                projectile.DisableProjectile();
            }

            if (bossProjectile != null)
            {
                bossProjectile.DisableBossProjectile();
            }

            SoundManager.Instance.PlaySound(SoundManager.Instance.ImpactClip, 0.1f);
            impactPS.Play();
            Invoke(nameof(Return), impactPS.main.duration);
        }
    }

    /*  REMOVE this method because we will put this into the MyLibrary class
    private bool CheckLayer(int layer, LayerMask objectMask)
    {
        return ((1 << layer) & objectMask) != 0;
    }
    */


/*

    private void OnEnable()
    {
        if (lifeTime > 0)
        {
            Invoke(nameof(Return), lifeTime);
        }

    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}

*/