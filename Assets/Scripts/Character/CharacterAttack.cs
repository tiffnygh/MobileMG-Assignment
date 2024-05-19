using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAttack : MonoBehaviour
{
    [Header("Bullet Type")]
    [SerializeField] private bool pierce;
    [SerializeField] private bool canHorinzontal;

    [Header("Spread Attack Settings")]
    [SerializeField] private bool canSpread;
    [SerializeField] private int numberOfProjectiles = 5;
    [SerializeField] private float spreadAngle = 45f;

    [SerializeField] private float bulletSpawnDistance = 1f;

    private  ObjectPooler Pooler;
    private Camera mainCamera;

    private Vector3 newMousePosition;
    private Vector3 mousePosition;

    public Vector3 ProjectileSpawnPosition { get; private set; }
    public Vector3 direction {  get; set; }

    private void Awake()
    {
        Pooler = GetComponent<ObjectPooler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBulletType();

        if (Input.GetMouseButtonDown(0))
        {
            if (canSpread)
            {
                ShootSpread();
            }
            else
            {
                ShootSingle();
            }
        }
    }

    private void FixedUpdate()
    {
        ProjectileSpawnPosition = GetSpawnProjectileDirectionAndPosition(GetWorldPosition());

    }
    public Vector3 GetWorldPosition()
    {
        mousePosition = Vector3.zero;
        if (Application.isMobilePlatform)
        {
            if (Input.touchCount > 0)
            {
                mousePosition = Input.GetTouch(0).position;
            }
        }
        else
        {
            mousePosition = Input.mousePosition;          
        }
        mousePosition.z = 1; 
        newMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        return newMousePosition;
    }

    public void CheckBulletType()
    {
        if (pierce)
        {
            foreach (GameObject obj in FindObjectsOfType<GameObject>())
            {
                if (obj.layer == LayerMask.NameToLayer("PlayerProjectile"))
                {
                    obj.GetComponent<ReturnToPool>().objectMask &= ~(1 << 8);
                }
            }
        }
        else
        {
            foreach (GameObject obj in FindObjectsOfType<GameObject>())
            {
                if (obj.layer == LayerMask.NameToLayer("PlayerProjectile"))
                {
                    obj.GetComponent<ReturnToPool>().objectMask |= (1 << 8); ;
                }
            }
        }

        if (canHorinzontal)
        {
            foreach (GameObject obj in FindObjectsOfType<GameObject>())
            {
                if (obj.layer == LayerMask.NameToLayer("PlayerProjectile"))
                {
                    obj.GetComponent<PlayerProjectile>().canHorizontal = true;
                }

            }
        }
        else
        {
            foreach (GameObject obj in FindObjectsOfType<GameObject>())
            {
                if (obj.layer == LayerMask.NameToLayer("PlayerProjectile"))
                {
                    obj.GetComponent<PlayerProjectile>().canHorizontal = false;
                }

            }
        }
    }

    public Vector3 GetSpawnProjectileDirectionAndPosition(Vector3 mousePosition)
    {
        direction = (mousePosition - transform.position).normalized;
        Vector3 spawnPosition = (transform.position + direction * bulletSpawnDistance);
        return spawnPosition;
    }
    public float GetAngleToMousePosition(Vector3 mousePosition)
    {
        direction = (mousePosition - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return angle;
    }
    private void ShootSingle()
    {

        GameObject projectile = Pooler.GetObjectFromPool();
        projectile.transform.position = ProjectileSpawnPosition;
        projectile.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        PlayerProjectile playerProjectile = projectile.GetComponent<PlayerProjectile>();
        playerProjectile.Direction = direction;
        playerProjectile.EnableProjectile();
    }


    private void ShootSpread()
    {
        Vector3 shootDirection = direction;
        float angleStep = spreadAngle / (numberOfProjectiles - 1);
        float startingAngle = -spreadAngle / 2;

        for (int i = 0; i < numberOfProjectiles; i++)
        {

            float currentAngle = startingAngle + (angleStep * i);
            Vector3 projectileDirection = Quaternion.Euler(0, 0, currentAngle) * shootDirection;

            GameObject projectile = Pooler.GetObjectFromPool();
            projectile.transform.position = ProjectileSpawnPosition;
            projectile.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg);
            //projectile.GetComponent<Rigidbody2D>().velocity = projectileDirection * projectile.GetComponent<PlayerProjectile>().Speed;

            PlayerProjectile playerProjectile = projectile.GetComponent<PlayerProjectile>();
            playerProjectile.Direction = projectileDirection;
            playerProjectile.EnableProjectile();
        }
    }

}
