using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public LayerMask objectMask;
    [SerializeField] private float lifeTime = 2f;

    [Header("Effects")]
    [SerializeField] private ParticleSystem impactPS;
    [SerializeField] private ParticleSystem freezePS;



    private PlayerProjectile playerProjectile;
    private HomingProjectile homingProjectile;

    private FreezeProjectile freezeProjectile;
    private SplitShotProjectile splitShotProjectile;


    private void Start()
    {
        playerProjectile = GetComponent<PlayerProjectile>();
        homingProjectile = GetComponent<HomingProjectile>();
        freezeProjectile = GetComponent<FreezeProjectile>();
        splitShotProjectile = GetComponent<SplitShotProjectile>();
    }

    // Returns this object to the pool
    private void Return()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (MyLibrary.CheckLayer(other.gameObject.layer, objectMask))
        {

            if (playerProjectile != null)
            {
                playerProjectile.DisableProjectile();
            }

            if (AttackManager.Instance.canFreeze)
            {
                freezePS.Play();
                Invoke(nameof(Return), AttackManager.Instance.freezeDuration + 0.2f);
            }
            else
            {
                impactPS.Play();
                Invoke(nameof(Return), impactPS.main.duration);
            }
        }
    }



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

