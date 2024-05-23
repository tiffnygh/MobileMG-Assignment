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

    private List<ObjectPooler> poolers = new List<ObjectPooler>();


    private bool allEnemiesDestroyed;

    [SerializeField] private int currWave;
    private int waveValue;

    private float waveTimer;
    [SerializeField] private float spawnInterval;
    private float spawnTimer;

    private SpawnGenerator spawnGenerator;

    //Direciton Integer  
    private int currentDirectionIndex = 0;
    private List<GameObject> currentSpawner; // Store the current spawner direction for the wave

    private void Awake()
    {
        spawnGenerator = GetComponentInParent<SpawnGenerator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializePools();
        GenerateWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
            currWave += 1;
            GenerateWave();
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
            newEnemy.transform.position = PositionToSpawn.transform.position;
            newEnemy.SetActive(true);
            newEnemy.GetComponent<EnemyMovement>().EnableEnemy();
            newEnemy.GetComponent<DropItem>().canDropItem = true;
            newEnemy.GetComponent<Health>().Revive();
            spawnedEnemies.Add(newEnemy);
            enemiesToSpawn.RemoveAt(0);
            spawnTimer = spawnInterval;
        }
    }
    //------------------------------------------------------------------------------GET SPAWNER DIRECTION---------------------------------------------------------------------------------------------

    public int GetDirectionIndex() //Thios func3tion get index that spawn enemy up - down - left - right 
    {
        int indexToReturn = currentDirectionIndex;
        currentDirectionIndex = (currentDirectionIndex + 1) % 4; // Cycle between 0 and 3
        return indexToReturn;
    }


    private List<GameObject> GetRandomDirection()
    {
        int randomDirection = Random.Range(0, 3);
        switch (randomDirection)
        {
            case 0:
                return spawnGenerator.topSpawners;
            case 1:
                return spawnGenerator.downSpawners;
            case 2:
                return spawnGenerator.leftSpawners;
            case 3:
                return spawnGenerator.rightSpawners;
            default:
                return spawnGenerator.allSpawners;
        }
    }

    private List<GameObject> GetDirectionSpawners(int direction)
    {
        switch (direction)
        {
            case 0:
                return spawnGenerator.topSpawners;
            case 1:
                return spawnGenerator.downSpawners;
            case 2:
                return spawnGenerator.leftSpawners;
            case 3:
                return spawnGenerator.rightSpawners;
            default:
                return null;
        }
    }

    //------------------------------------------------------------------------------GENERATE ENEMY---------------------------------------------------------------------------------------------

    public void GenerateWave()
    {
        waveValue = currWave * 10;
        GenerateEnemies();
        if (enemiesToSpawn.Count == 0)
        {
            return;
        }
        waveTimer = spawnInterval * currWave * 10 + 15;
        currentSpawner = GetDirectionSpawners(GetDirectionIndex()); //This line change the direction of spawner when generate wave 
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
