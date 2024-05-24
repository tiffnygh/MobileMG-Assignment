using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] private List<WaveSpawner> allSpawners = new List<WaveSpawner>();
    [SerializeField] private int startingWaves = 1;

    private int currentWave = 0;
    private float  waveTimer { get; set; }

    [Header("Spawners Setting")]
    public float waveDuration;
    public float spawnInterval;
    public float waveValue;
    [SerializeField] private float delayBeforeNextWave;


    private Dictionary<string, WaveSpawner> spawnerDict = new Dictionary<string, WaveSpawner>();

    protected override void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeSpawners();
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (waveTimer > 0)
        {
            waveTimer -= Time.deltaTime;
            if (waveTimer <= 0)
            {
                CheckWaveCompletion();
                OnWaveCompleted();
            }
        }
    }

    private void InitializeSpawners()
    {
        // Populate the dictionary with spawner names and their corresponding instances
        foreach (var spawner in allSpawners)
        {
            spawnerDict[spawner.gameObject.name] = spawner;
        }
    }

    private void StartWave()
    {
        currentWave++;
        waveDuration = spawnInterval * waveValue + delayBeforeNextWave;
        waveTimer = waveDuration;
        Debug.Log("Starting Wave: " + currentWave);

        // Enable spawners based on the current wave
        EnableSpawnersForWave(currentWave);
    }

    private void EnableSpawnersForWave(int wave)
    {
        // Example logic to enable specific spawners based on the wave number
        // You can customize this logic as per your requirements

        // Start with enabling the basic tiny spawners
        if (wave == 1)
        {
            EnableRandomSpawner(5);
            //EnableSpawnerByName("GreyTinyEnemySpawner");
        }
        else if (wave == 2)
        {
            EnableSpawnerByName("GreenTinyEnemySpawner");
            EnableSpawnerByName("YellowTinyEnemySpawner");
        }
        else if (wave == 3)
        {
            EnableSpawnerByName("GreenTinyEnemySpawner");
            EnableSpawnerByName("YellowTinyEnemySpawner");
            EnableSpawnerByName("PinkTinyEnemySpawner");
        }
        else if (wave == 4)
        {
            EnableSpawnerByName("GreenTinyEnemySpawner");
            EnableSpawnerByName("YellowTinyEnemySpawner");
            EnableSpawnerByName("PinkTinyEnemySpawner");
            EnableSpawnerByName("PurpleTinyEnemySpawner");
        }
        // After a certain number of waves, start enabling medium spawners
        else if (wave >= 5)
        {
            EnableSpawnerByName("GreenTinyEnemySpawner");
            EnableSpawnerByName("YellowTinyEnemySpawner");
            EnableSpawnerByName("PinkTinyEnemySpawner");
            EnableSpawnerByName("PurpleTinyEnemySpawner");

            EnableSpawnerByName("GreenMediumEnemySpawner");
            EnableSpawnerByName("YellowMediumEnemySpawner");
            EnableSpawnerByName("PinkMediumEnemySpawner");
            EnableSpawnerByName("PurpleMediumEnemySpawner");
        }
    }
    //----------------------------------------------------------------------Enable Spawner functions--------------------------------------------
    private void EnableRandomTinySpawner(int numberOfSpawners)
    {
        List<string> spawnerNames = new List<string>()
        {
            "GreyTinyEnemySpawner",
            "GreenTinyEnemySpawner",
            "YellowTinyEnemySpawner",
            "PinkTinyEnemySpawner",
            "PurpleTinyEnemySpawner"
        };


        for (int i = 0; i < numberOfSpawners; i++)
        {
            if (spawnerNames.Count == 0) break; // No more spawners to enable

            string randomSpawnerName = spawnerNames[Random.Range(0, spawnerNames.Count)];

            EnableSpawnerByName(randomSpawnerName);
            spawnerNames.Remove(randomSpawnerName); // Remove enabled spawner from the list to avoid enabling it again
        }
    }

    private void EnableRandomMediumSpawner(int numberOfSpawners)
    {
        List<string> spawnerNames = new List<string>()
        {
            "GreyMediumEnemySpawner",
            "GreenMediumEnemySpawner",
            "YellowMediumEnemySpawner",
            "PinkMediumEnemySpawner",
            "PurpleMediumEnemySpawner"
        };


        for (int i = 0; i < numberOfSpawners; i++)
        {
            if (spawnerNames.Count == 0) break; // No more spawners to enable

            string randomSpawnerName = spawnerNames[Random.Range(0, spawnerNames.Count)];

            EnableSpawnerByName(randomSpawnerName);
            spawnerNames.Remove(randomSpawnerName); // Remove enabled spawner from the list to avoid enabling it again
        }
    }

    private void EnableRandomSpawner(int numberOfSpawners)
    {
        List<string> spawnerNames = new List<string>()
        {
            "GreyTinyEnemySpawner",
            "GreenTinyEnemySpawner",
            "YellowTinyEnemySpawner",
            "PinkTinyEnemySpawner",
            "PurpleTinyEnemySpawner",
            "GreyMediumEnemySpawner",
            "GreenMediumEnemySpawner",
            "YellowMediumEnemySpawner",
            "PinkMediumEnemySpawner",
            "PurpleMediumEnemySpawner"
        };


        for (int i = 0; i < numberOfSpawners; i++)
        {
            if (spawnerNames.Count == 0) break; // No more spawners to enable

            string randomSpawnerName = spawnerNames[Random.Range(0, spawnerNames.Count)];

            EnableSpawnerByName(randomSpawnerName);
            spawnerNames.Remove(randomSpawnerName); // Remove enabled spawner from the list to avoid enabling it again
        }
    }



    private void EnableSpawnerByName(string spawnerName)
    {
        if (spawnerDict.TryGetValue(spawnerName, out var spawner))
        {
            spawner.GenerateWave();
        }
        else
        {
            Debug.LogWarning("Spawner with name " + spawnerName + " not found!");
        }
    }
    //-------------------------------------------------------Check Wave End----------------------------------------------------------------------
    private bool AreAllEnemiesDestroyed()
    {
        foreach (var spawner in allSpawners)
        {
            if (spawner.CheckAllEnemiesDestroyed() == false)
            {
                return false;
            }
        }
        return true;
    }

    private void CheckWaveCompletion()
    {
        if (AreAllEnemiesDestroyed())
        {
            Debug.Log("Grace Reward"); // Award grace reward
        }
        else
        {
            Debug.Log("NOOOOOOOOOOOOOO REWARDDDDDDDDDDDDDDDDDDDDDDDD"); // Award grace reward
        }
    }

    public void OnWaveCompleted()
    {
        StartWave();
    }
}
