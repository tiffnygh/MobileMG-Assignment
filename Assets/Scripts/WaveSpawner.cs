using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPoints;

    [SerializeField] private List<Enemy> enemyList = new List<Enemy>();
    [SerializeField] private List<GameObject> enemiesToSpawn = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();

    private List<ObjectPooler> poolers = new List<ObjectPooler>();


    private bool allEnemiesDestroyed;

    public int currWave;
    public int waveValue;

    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

    private SpawnGenerator spawnGenerator;


    // Start is called before the first frame update
    void Start()
    {
        InitializePools();
        GenerateWave();
        spawnGenerator = GetComponent<SpawnGenerator>();
        spawnPoints = spawnGenerator.allSpawners;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckAllEnemiesDestroyed();


        if (spawnTimer <= 0)
        {
            if(enemiesToSpawn.Count > 0)
            {
                int randomIndex = Random.Range(0, spawnPoints.Count);
                GameObject spawnPoint = spawnPoints[randomIndex];
                SpawnEnemy(spawnPoint);
            }
            else
            {
                waveTimer = 0;
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }   
    }

    private void InitializePools()
    {
        foreach (var enemy in enemyList)
        {
            Transform matchingChild = transform.Find(enemy.enemyPrefab.name);
            // Assume each enemy prefab has an ObjectPooler component attached
            var pooler = matchingChild.GetComponent<ObjectPooler>();
            //pooler.GetObjectFromPool();
            poolers.Add(pooler);
        }
    }

    private void SpawnEnemy(GameObject PositionToSpawn)
    {
        var enemyPrefab = enemiesToSpawn[0]; // The prefab to spawn
        var pooler = poolers.Find(p => p.objectPrefab == enemyPrefab); // Find corresponding pooler
        var newEnemy = pooler.GetObjectFromPool(); // Get object from pool

        if (newEnemy != null)
        {
            newEnemy.transform.position = PositionToSpawn.transform.position;
            newEnemy.SetActive(true);
            newEnemy.GetComponent<EnemyMovement>().EnableEnemy();
            newEnemy.GetComponent<Health>().Revive();
            spawnedEnemies.Add(newEnemy);
            enemiesToSpawn.RemoveAt(0);
            spawnTimer = spawnInterval;
        }
    }

    public void GenerateWave()
    {
        waveValue = currWave * 10;
        GenerateEnemies();
        if (enemiesToSpawn.Count == 0)
        {
            return;
        }
        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
    }

    public void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>(); 
        while(waveValue > 0)
        {
            int randEnemyId = Random.Range(0, enemyList.Count);
            int randEnemyCost = enemyList[randEnemyId].cost;

            if(waveValue - randEnemyCost >= 0)
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

    private bool CheckAllEnemiesDestroyed()
    {
        for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = spawnedEnemies[i];
            if (!enemy.activeSelf) // If any enemy is alive, return false
            {
                spawnedEnemies.Remove(enemy);
                Debug.Log("Remove Enemy");
            }   
        }

        if (spawnedEnemies.Count == 0)
        {
            Debug.Log("No More Enemy");
            return true;
        }

        Debug.Log("Still Got Enemy");
        return false;
    }


    [System.Serializable]
    public class Enemy
    {
        public GameObject enemyPrefab;
        public int cost;
    }
}
