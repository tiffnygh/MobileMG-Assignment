using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public GameObject prefab;
    public ObjectPooler Pooler;

    private Camera mainCamera;
    private Vector3 newMousePosition;
    private Vector3 mousePosition;

    public Vector3 ProjectileSpawnPosition { get; private set; }
    public Vector3 direction {  get; set; }

    private void Awake()
    {
        Pooler = Pooler.GetComponent<ObjectPooler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject projectile = Pooler.GetObjectFromPool();
            projectile.GetComponent<PlayerProjectile>().EnableProjectile();
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



    public Vector3 GetSpawnProjectileDirectionAndPosition(Vector3 mousePosition)
    {
        direction = (mousePosition - transform.position).normalized;
        Vector3 spawnPosition = (transform.position + direction * 2).normalized;
        return spawnPosition;
    }
}
