using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : Singleton<AttackManager>
{
    [Header("Bullet Type")]
    [SerializeField] public bool pierce;
    [SerializeField] public bool canHorinzontal;

    [Header("Spread Attack Settings")]
    [SerializeField] public bool canSpread;
    [SerializeField] public int numberOfProjectiles = 5;
    [SerializeField] public float spreadAngle = 45f;

    [Header("Upgrade Attack Settings")]
    public bool canAOE;
    public bool canFreeze;

    [SerializeField] public float bulletSpawnDistance = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
