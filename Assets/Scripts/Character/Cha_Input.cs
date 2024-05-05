using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cha_Input : MonoBehaviour
{
    public GameObject prefab;

    private Camera mainCamera;
    private Vector3 directionPosition;
    private Vector3 mousePosition;

    public Vector3 ProjectileSpawnPosition { get; private set; }
    public Vector3 direction {  get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ProjectileSpawnPosition = GetSpawnProjectilePosition(GetWorldPosition());
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(prefab, ProjectileSpawnPosition, transform.rotation);
        }
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
        mousePosition.z = 1; // Ensure the z-position is not changing if you're using 2D
        directionPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        return directionPosition;
    }



    public Vector3 GetSpawnProjectilePosition(Vector3 targetPosition)
    {
        direction = (targetPosition - transform.position).normalized;
        Vector3 spawnPosition = (transform.position + direction * 2).normalized;
        return spawnPosition;
    }
}
