using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : Singleton<AttackManager>
{
    [Header("General Setting")]
    [SerializeField] public int damage;
    [SerializeField] public float speed = 5f;
    [SerializeField] public float acceleration = 0f;

    [SerializeField] public Vector3 scale;


    /*[Header("Horizontal Movement Settings")]
    [SerializeField] public bool canHorizontal;
    [SerializeField] public float horizontalSpeed = 5f;
    [SerializeField] public float horizontalDistance = 2f;
    */
    [Header("Bullet Type")]
    [SerializeField] public bool pierce;
    [SerializeField] public bool canHorinzontal;

    [Header("Spread Attack Settings")]
    [SerializeField] public bool canSpread;
    [SerializeField] public int numberOfProjectiles = 5;
    [SerializeField] public float spreadAngle = 45f;

    [Header("AOE Settings")]
    public bool canAOE;
    public float aoeCooldownDuration;
    public float explosionRadius = 5f;
    public int explosionDamage;

    [Header("Freeze Settings")]
    public bool canFreeze;
    public float freezeCooldownDuration;
    public float freezeDuration = 3f;

    [Header("Settings")]
    public float bulletSpawnDistance = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
