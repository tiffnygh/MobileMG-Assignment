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


    private PlayerProjectile playerProjectile;
    private HomingProjectile homingProjectile;

    private FreezeProjectile freezeProjectile;


    private void Start()
    {
        playerProjectile = GetComponent<PlayerProjectile>();
        homingProjectile = GetComponent<HomingProjectile>();
        freezeProjectile = GetComponent<FreezeProjectile>();
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

            if (homingProjectile != null)
            {
                homingProjectile.DisableProjectile();
            }

            //SoundManager.Instance.PlaySound(SoundManager.Instance.ImpactClip, 0.1f);
            //impactPS.Play();
            if (freezeProjectile != null)
            {
                Invoke(nameof(Return), freezeProjectile.freezeDuration + 0.2f);
            }
            else
            {
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

