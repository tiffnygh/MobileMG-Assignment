using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    [SerializeField] private List<Enemy> enemyList = new List<Enemy>();
    [SerializeField] private List<GameObject> enemiesToSpawn = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();
    private HashSet<GameObject> uniqueSpawnedEnemies = new HashSet<GameObject>();


    private List<ObjectPooler> poolers = new List<ObjectPooler>();


    private bool allEnemiesDestroyed;
    private bool waveCompleted;
    private bool isActive;

    private float waveValue { get; set; }

    private float waveTimer { get; set; }
    private float spawnInterval { get; set; }

    private float spawnTimer { get; set; }

    //Direciton Integer  
    private int currentDirectionIndex = 0;
    private List<GameObject> currentSpawner; // Store the current spawner direction for the wave

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

                    //THis code spawn in all direction = random direction
                    /*int randomIndex = Random.Range(0, spawnGenerator.allSpawners.Count);
                    GameObject spawnPoint = spawnGenerator.allSpawners[randomIndex];
                    SpawnEnemy(spawnPoint);*/

                    //This is test to spawn in up down left right in order
                    /*List<GameObject> newCurrentSpawner = GetDirectionSpawners(GetDirectionIndex());
                    int randomIndex = Random.Range(0, newCurrentSpawner.Count);
                    GameObject spawnPoint = newCurrentSpawner[randomIndex];
                    SpawnEnemy(spawnPoint);*/

                    //This code spawn in 1 direction, and change only when changing wave 
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
            //GenerateWave();
            //Try disabling after timer end, not sure if enemy will bug out
            //this.gameObject.SetActive(false);
            /*if (CheckAllEnemiesDestroyed())
            {
                Debug.Log("Grace Reward"); //If player kill the enemy before the grace period, then they get bonus reward
            }*/
            //waveCompleted = true;
            isActive = false;
            //WaveManager.Instance.OnWaveCompleted();


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
                //uniqueSpawnedEnemies.Add(newEnemy);
            }
            enemiesToSpawn.RemoveAt(0);
            spawnTimer = spawnInterval;
        }
    }
    //------------------------------------------------------------------------------GET SPAWNER DIRECTION---------------------------------------------------------------------------------------------



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

    private void OnEnable()
    {
    }


    [System.Serializable]
    public class Enemy
    {
        public GameObject enemyPrefab;
        public int cost;
    }
}
