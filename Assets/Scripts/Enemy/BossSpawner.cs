using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private List<WaveSpawner.Enemy> enemyList = new List<WaveSpawner.Enemy>();
    [SerializeField] private List<GameObject> enemiesToSpawn = new List<GameObject>();
    private List<ObjectPooler> poolers = new List<ObjectPooler>();

    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float enemyValue = 100f;

    private float spawnTimer;
    private bool isActive;

    private void Awake()
    {
    }

    private void Start()
    {
        InitializePools();
        isActive = true;
        spawnTimer = spawnInterval;
        ActivateBossSpawner();
    }

    private void Update()
    {
        if (!isActive) return;

        if (spawnTimer <= 0)
        {
            if (enemiesToSpawn.Count > 0)
            {
                SpawnEnemy();
                spawnTimer = spawnInterval;
            }
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }
    }

    private void InitializePools()
    {
        foreach (var enemy in enemyList)
        {
            GameObject matchingGameObject = GameObject.Find(enemy.enemyPrefab.name);
            var pooler = matchingGameObject.GetComponent<ObjectPooler>();
            poolers.Add(pooler);
        }
    }

    private void SpawnEnemy()
    {
        var enemyPrefab = enemiesToSpawn[0];
        var pooler = poolers.Find(p => p.objectPrefab == enemyPrefab);
        var newEnemy = pooler.GetObjectFromPool();

        if (newEnemy != null)
        {
            newEnemy.transform.position = transform.position;
            newEnemy.SetActive(true);
            newEnemy.GetComponent<EnemyMovement>().EnableEnemy();
            newEnemy.GetComponent<DropItem>().canDropItem = true;
            newEnemy.GetComponent<Health>().Revive();
        }
        enemiesToSpawn.RemoveAt(0);
    }

    public void ActivateBossSpawner()
    {
        GenerateEnemies();
        isActive = true;
    }

    public void DeactivateBossSpawner()
    {
        isActive = false;
    }

    private void GenerateEnemies()
    {
        float waveValue = enemyValue;
        List<GameObject> generatedEnemies = new List<GameObject>();

        while (waveValue > 0)
        {
            int randEnemyId = Random.Range(0, enemyList.Count);
            int randEnemyCost = enemyList[randEnemyId].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemyList[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }
}
