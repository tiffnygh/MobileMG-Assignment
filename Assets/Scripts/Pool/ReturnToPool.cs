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
    [SerializeField] private ParticleSystem aoePS;
    [SerializeField] private ParticleSystem freezeAoePS;


    [Header("Effects")]
    [SerializeField] private GameObject impactPSGameObject;
    [SerializeField] private GameObject freezePSGameObject;
    [SerializeField] private GameObject aoePSGameObject;
    [SerializeField] private GameObject freezeAoePSGameObject;





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

            if (AttackManager.Instance.canFreeze && !AttackManager.Instance.canAOE)
            {
                freezePS.Play();
                Invoke(nameof(Return), AttackManager.Instance.freezeDuration + 0.2f);               
            }
            else if (AttackManager.Instance.canFreeze && AttackManager.Instance.canAOE)
            {
                AdjustParticleSystemScale(freezeAoePS);
                freezeAoePS.Play();
                Invoke(nameof(Return), AttackManager.Instance.freezeDuration + 0.2f);
            }
            else if (AttackManager.Instance.canAOE && !AttackManager.Instance.canFreeze)
            {
                AdjustParticleSystemScale(aoePS);
                aoePS.Play();
                Invoke(nameof(Return), impactPS.main.duration);
            }
            else
            {

                impactPS.Play();
                Invoke(nameof(Return), impactPS.main.duration);
            }
        }
        else
        {
            if (AttackManager.Instance.canFreeze && !AttackManager.Instance.canAOE)
            {
                if (AttackManager.Instance.pierce)
                {
                    Instantiate(freezePSGameObject, other.transform.position, Quaternion.identity);
                }
            }
            else if (AttackManager.Instance.canFreeze && AttackManager.Instance.canAOE)
            {
                if (AttackManager.Instance.pierce)
                {
                    AdjustPrefabScale(freezeAoePSGameObject);
                    Instantiate(freezeAoePSGameObject, other.transform.position, Quaternion.identity);
                }
            }
            else if (AttackManager.Instance.canAOE && !AttackManager.Instance.canFreeze)
            {
                if (AttackManager.Instance.pierce)
                {
                    AdjustPrefabScale(aoePSGameObject);
                    Instantiate(aoePSGameObject, other.transform.position, Quaternion.identity);
                }
            }
            else
            {
                if (AttackManager.Instance.pierce && !AttackManager.Instance.canFreeze && !AttackManager.Instance.canAOE)
                {
                    Instantiate(impactPSGameObject, other.transform.position, Quaternion.identity);
                }
            }
        }
    }

    private void AdjustPrefabScale(GameObject prefabInstance)
    {
        float scale = AttackManager.Instance.explosionRadius * 2;
        prefabInstance.transform.localScale = new Vector3(scale, scale, scale);
    }

    private void AdjustParticleSystemScale(ParticleSystem particleSystem)
    {
        float scale = AttackManager.Instance.explosionRadius * 2;

        GameObject main = particleSystem.gameObject;
        main.transform.localScale = new Vector3(scale, scale, scale);
        // If you have other scaling properties, adjust them accordingly
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

