using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    [SerializeField] private Character.CharacterTypes damageType = Character.CharacterTypes.AI;
    
    public int DamageToApply {  get; set; }

    private Health enemyHealth;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        enemyHealth = GetComponent<Health>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && other.gameObject.layer != 10) //PlayerProjectile layer
        {
            DamageToApply = other.GetComponent<PlayerProjectile>().Damage;
            enemyHealth.TakeDamage(DamageToApply);
            //SoundManager.Instance.PlaySound(SoundManager.Instance.ImpactClip, 0.1f);
        }
    }

}
