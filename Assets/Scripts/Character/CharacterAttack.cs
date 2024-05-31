using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterAttack : MonoBehaviour
{

    public  ObjectPooler Pooler;
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
        if(Application.isMobilePlatform)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began && !IsPointerOverUIObject(touch.fingerId))
                {
                    ProjectileSpawnPosition = GetSpawnProjectileDirectionAndPosition(GetWorldPosition(touch.position));
                    if (AttackManager.Instance.canSpread)
                    {
                        ShootSpread();
                    }
                    else
                    {
                        ShootSingle();
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
            {
                ProjectileSpawnPosition = GetSpawnProjectileDirectionAndPosition(GetWorldPosition(Input.mousePosition));
                if (AttackManager.Instance.canSpread)
                {
                    ShootSpread();
                }
                else
                {
                    ShootSingle();
                }
            }
        }
    }

    private void FixedUpdate()
    {

    }
    public Vector3 GetWorldPosition(Vector3 inputPosition)
    {
        inputPosition.z = 1; // Set this to the correct distance from the camera
        newMousePosition = mainCamera.ScreenToWorldPoint(inputPosition);
        return newMousePosition;
    }
    /*
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
    }*/

    public void CheckBulletType()
    {
        if (AttackManager.Instance.pierce)
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

        if (AttackManager.Instance.canHorinzontal)
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

    private bool IsPointerOverUIObject(int fingerId = -1)
    {
        if (fingerId >= 0)
        {
            return EventSystem.current.IsPointerOverGameObject(fingerId);
        }
        return EventSystem.current.IsPointerOverGameObject();
    }

    public Vector3 GetSpawnProjectileDirectionAndPosition(Vector3 mousePosition)
    {
        direction = (mousePosition - transform.position).normalized;
        Vector3 spawnPosition = (transform.position + direction * AttackManager.Instance.bulletSpawnDistance);
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
        SoundManager.Instance.PlaySound(SoundManager.Instance.DefaultShootClip, 1f);
        GameObject projectile = Pooler.GetObjectFromPool();
        projectile.transform.position = ProjectileSpawnPosition;
        projectile.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        PlayerProjectile playerProjectile = projectile.GetComponent<PlayerProjectile>();
        playerProjectile.Direction = direction;
        playerProjectile.EnableProjectile();
    }


    private void ShootSpread()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.DefaultShootClip, 1f);
        Vector3 shootDirection = direction;
        float angleStep = AttackManager.Instance.spreadAngle / (AttackManager.Instance.numberOfProjectiles - 1);
        float startingAngle = -AttackManager.Instance.spreadAngle / 2;

        for (int i = 0; i < AttackManager.Instance.numberOfProjectiles; i++)
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
