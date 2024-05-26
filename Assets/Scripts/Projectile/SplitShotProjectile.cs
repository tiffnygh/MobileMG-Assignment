using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitShotProjectile : MonoBehaviour
{

    [Header("Split Settings")]
    [SerializeField] private int numberOfSplits = 5;
    [SerializeField] private float splitDelay = 0.5f;
    [SerializeField] private float splitSpeed = 100f;

    private bool hasSplit = false;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasSplit && collision.CompareTag("Enemy") && collision.gameObject.layer != 9)
        {
            collision.GetComponent<Health>().TakeDamage(projectile.Damage);
            Split();
        }
        else if (hasSplit && collision.CompareTag("Enemy") && collision.gameObject.layer != 9)
        {
            collision.GetComponent<Health>().TakeDamage(projectile.Damage);
        }
    }

    private void Split()
    {
        hasSplit = true;

        for (int i = 0; i < numberOfSplits; i++)
        {
            GameObject newProjectile = characterAttack.Pooler.GetObjectFromPool();

            Vector2 splitDirection = Random.insideUnitCircle.normalized;
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(splitDirection.y, splitDirection.x) * Mathf.Rad2Deg);

            newProjectile.GetComponent<PlayerProjectile>().Direction = splitDirection;
            newProjectile.GetComponent<SplitShotProjectile>().hasSplit = true;
            newProjectile.GetComponent<PlayerProjectile>().Speed = splitSpeed;
            newProjectile.GetComponent<PlayerProjectile>().EnableProjectile();
        }

        // Optionally, play split animation/sound here

        // Return the original projectile to the pool
        hasSplit = false;

    }

}
