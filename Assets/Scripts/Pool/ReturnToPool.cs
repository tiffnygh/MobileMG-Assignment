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


    private void Start()
    {
        playerProjectile = GetComponent<PlayerProjectile>();
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

            //SoundManager.Instance.PlaySound(SoundManager.Instance.ImpactClip, 0.1f);
            //impactPS.Play();
            Invoke(nameof(Return), impactPS.main.duration);
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

