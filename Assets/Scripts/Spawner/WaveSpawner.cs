using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    [SerializeField] private List<Enemy> enemyList = new List<Enemy>();
    [SerializeField] private List<GameObject> enemiesToSpawn = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();
    private HashSet<GameObject> uniqueSpawnedEnemies = new HashSet<GameObject>();


    private List<ObjectPooler> poolers = new List<ObjectPooler>();

    private bool isActive;
    private float waveValue { get; set; }
    private float waveTimer { get; set; }
    private float spawnInterval { get; set; }

    private float spawnTimer { get; set; }

    private List<GameObject> currentSpawner { get; set; }// Store the current spawner direction for the wave

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializePools();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isActive) return;

        CheckAllEnemiesDestroyed();
        if (waveTimer >= 0)
        {
            if (spawnTimer <= 0)
            {
                if (enemiesToSpawn.Count > 0)
                {
                    if (currentSpawner.Count == 0)
                    {
                        currentSpawner = WaveManager.Instance.GetRandomDirection(1);
                    }

                    int randomIndex = Random.Range(0, currentSpawner.Count);
                    GameObject spawnPoint = currentSpawner[randomIndex];
                    SpawnEnemy(spawnPoint);
                }
                else
                {
                    waveTimer -= Time.fixedDeltaTime;
                }
            }
            else
            {
                spawnTimer -= Time.fixedDeltaTime;
                waveTimer -= Time.fixedDeltaTime;
            }
        }
        else
        {
            isActive = false;
        }
    }

    private void InitializePools()
    {
        foreach (var enemy in enemyList)
        {
            GameObject matchingGameObject = GameObject.Find(enemy.enemyPrefab.name);
            // Assume each enemy prefab has an ObjectPooler component attached
            var pooler = matchingGameObject.GetComponent<ObjectPooler>();
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
            if (!uniqueSpawnedEnemies.Contains(newEnemy))
            {
                newEnemy.transform.position = PositionToSpawn.transform.position;
                newEnemy.SetActive(true);
                newEnemy.GetComponent<EnemyMovement>().EnableEnemy();
                newEnemy.GetComponent<DropItem>().canDropItem = true;
                newEnemy.GetComponent<Health>().Revive();
                spawnedEnemies.Add(newEnemy);
            }
            enemiesToSpawn.RemoveAt(0);
            spawnTimer = spawnInterval;
        }
    }



    //------------------------------------------------------------------------------GENERATE ENEMY---------------------------------------------------------------------------------------------

    public void GenerateWave()
    {
        //currWave += 1; This should be call in wave manager instead, so that the difficultly progression make more sense 
        waveValue = WaveManager.Instance.waveValue;
        GenerateEnemies();
        if (enemiesToSpawn.Count == 0)
        {
            return;
        }
        waveTimer = WaveManager.Instance.waveDuration;
        spawnInterval = WaveManager.Instance.spawnInterval;
        currentSpawner = WaveManager.Instance.currentSpawnPositions; //This line change the direction of spawner when generate wave 

        isActive = true; // Set the spawner to active
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

    public bool CheckAllEnemiesDestroyed()
    {
        for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = spawnedEnemies[i];
            if (!enemy.activeSelf) // If any enemy is alive, return false
            {
                spawnedEnemies.Remove(enemy);
                //uniqueSpawnedEnemies.Remove(enemy);
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
