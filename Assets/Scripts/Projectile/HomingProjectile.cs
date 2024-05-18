using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HomingProjectile : PlayerProjectile
{
    [Header("Homing Settings")]
    [SerializeField] private float homingSpeed = 2f;
    [SerializeField] private float homingRadius = 5f;

    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            AdjustDirectionTowardsTarget();
        }
        else
        {
            FindTarget();
        }
    }

    private void FindTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, homingRadius);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = collider.transform;
                }
            }
        }

        if (closestEnemy != null)
        {
            target = closestEnemy;
        }
    }

    private void AdjustDirectionTowardsTarget()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        Direction = Vector3.Lerp(Direction, directionToTarget, homingSpeed * Time.deltaTime).normalized;
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.LookRotation(Direction);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    public override void MoveProjectile()
    {
        if (target != null)
        {
            AdjustDirectionTowardsTarget();
        }

        base.MoveProjectile();
    }

    public override void EnableProjectile()
    {
        FindTarget();
        base.EnableProjectile();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, homingRadius);
    }
}
