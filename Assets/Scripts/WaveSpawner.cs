using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();

    public List<Enemy> enemyList = new List<Enemy>();
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    private bool allEnemiesDestroyed;

    public int currWave;
    public int waveValue;

    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;



    // Start is called before the first frame update
    void Start()
    {
        GenerateWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (Input.GetKeyDown(KeyCode.O))
        {
            CheckAllEnemiesDestroyed();
        }

        if (spawnTimer <= 0)
        {
            if(enemiesToSpawn.Count > 0)
            {
                int randomIndex = Random.Range(0, spawnPoints.Count);
                Transform spawnPoint = spawnPoints[randomIndex];

                GameObject newEnemy = Instantiate(enemiesToSpawn[0], spawnPoint.position, Quaternion.identity);
                newEnemy.transform.parent = this.transform;
                spawnedEnemies.Add(newEnemy);
                enemiesToSpawn.RemoveAt(0);
                spawnTimer = spawnInterval;
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

    public void GenerateWave()
    {
        waveValue = currWave * 10;
        GenerateEnemies();

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
