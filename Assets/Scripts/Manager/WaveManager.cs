using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] private List<WaveSpawner> allSpawners = new List<WaveSpawner>();

    private int currentWave = 0;
    private float  waveTimer { get; set; }

    [Header("Spawners Setting")]
    public float waveDuration;
    public float spawnInterval;
    public float waveValue;

    [SerializeField] private float delayBeforeNextWave;


    private Dictionary<string, WaveSpawner> spawnerDict = new Dictionary<string, WaveSpawner>();

    private SpawnGenerator spawnGenerator;

    //Enemy spawner positions
    private List<GameObject> thisWaveCurrentSpawnPositions { get; set; } // Store the current spawner direction for the wave
    
    public List<GameObject> currentSpawnPositions; // Store the current spawner direction for the wave

    private int numberOfDirection = 1;


    protected override void Awake()
    {
        spawnGenerator = GetComponent<SpawnGenerator>();
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
    private void SetTimerAndInterval(float interval)
    {
        spawnInterval = interval;
        waveDuration = spawnInterval * waveValue + delayBeforeNextWave;
        waveTimer = waveDuration;
    }

    private void StartWave()
    {
        currentWave++;
        numberOfDirection++;
        
        Debug.Log("Starting Wave: " + currentWave);

        // Enable spawners based on the current wave
        EnableSpawnersForWave(currentWave);
    }

    private float ReduceSpawnInterval()
    {
        spawnInterval = Mathf.Max(0.5f, spawnInterval - 0.03f);
        return spawnInterval;

    }
    
    private void EnableSpawnersForWave(int wave)
    {
        currentSpawnPositions = GetRandomDirection(CalculateNumberOfDirections(wave));
        //currentSpawnPositions = GetRandomDirection(CalculateNumberOfDirections(wave));

        if (wave >= 1 && wave <= 5)
        {
            SetTimerAndInterval(0.5f);
            if (wave == 1) EnableSpawnerByName("GreyTinyEnemySpawner");
            if (wave == 2) EnableSpawnerByName("YellowTinyEnemySpawner");
            if (wave == 3) EnableSpawnerByName("GreenTinyEnemySpawner");
            if (wave == 4) EnableSpawnerByName("PurpleTinyEnemySpawner");
            if (wave == 5) EnableSpawnerByName("PinkTinyEnemySpawner");
        }
        else if (wave >= 6 && wave <= 10)
        {
            SetTimerAndInterval(1f);
            if (wave == 6) EnableRandomTinySpawner(1);
            if (wave == 7) EnableRandomTinySpawner(2);
            if (wave == 8) EnableRandomTinySpawner(2);
            if (wave == 9) EnableRandomTinySpawner(3);
            if (wave == 10) EnableRandomTinySpawner(4);
        }
        else if (wave >= 11 && wave <= 15)
        {
            SetTimerAndInterval(0.5f);
            if (wave == 11) EnableSpawnerByName("GreyMediumEnemySpawner");
            if (wave == 12) EnableSpawnerByName("YellowMediumEnemySpawner");
            if (wave == 13) EnableSpawnerByName("GreenMediumEnemySpawner");
            if (wave == 14) EnableSpawnerByName("PurpleMediumEnemySpawner");
            if (wave == 15) EnableSpawnerByName("PinkMediumEnemySpawner");
        }
        else if (wave >= 16 && wave <= 20)
        {
            SetTimerAndInterval(1f);
            if (wave == 16) EnableRandomMediumSpawner(1);
            if (wave == 17) EnableRandomMediumSpawner(2);
            if (wave == 18) EnableRandomMediumSpawner(2);
            if (wave == 19) EnableRandomMediumSpawner(3);
            if (wave == 20) EnableRandomMediumSpawner(3);
        }
        else if (wave >= 21 && wave <= 25)
        {
            SetTimerAndInterval(ReduceSpawnInterval());
            if (wave == 21) EnableRandomTinySpawner(1); EnableRandomMediumSpawner(1);
            if (wave == 22) EnableRandomTinySpawner(1); EnableRandomMediumSpawner(1);
            if (wave == 23) EnableRandomTinySpawner(2); EnableRandomMediumSpawner(1);
            if (wave == 24) EnableRandomTinySpawner(2); EnableRandomMediumSpawner(1);
            if (wave == 25) EnableRandomTinySpawner(2); EnableRandomMediumSpawner(2);
        }
        else if (wave >= 26 && wave <= 30)
        {
            SetTimerAndInterval(ReduceSpawnInterval());

            if (wave == 26) EnableRandomSpawner(3);
            if (wave == 27) EnableRandomSpawner(3);
            if (wave == 28) EnableRandomSpawner(4);
            if (wave == 29) EnableRandomSpawner(4);
            if (wave == 30) EnableRandomSpawner(5);
        }
        else if (wave >= 31 && wave <= 40)
        {
            SetTimerAndInterval(ReduceSpawnInterval());

            EnableRandomSpawner(6);
        }
        else if (wave >= 41 && wave <= 49)
        {
            SetTimerAndInterval(ReduceSpawnInterval());

            EnableRandomSpawner(7);
        }
        else if (wave == 50)
        {
            EnableRandomSpawner(7); //Change to boss wave?
        }
        else if (wave >= 51)
        {
            SetTimerAndInterval(ReduceSpawnInterval());

            EnableRandomSpawner(Random.Range(5,10));
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
    //-------------------------------------------------------Get Spawner Direction----------------------------------------------------------------------

    public List<GameObject> GetRandomDirection(int numberOfDirection)
    {
        List<List<GameObject>> allDirections = new List<List<GameObject>>()
        {
            spawnGenerator.topSpawners,
            spawnGenerator.downSpawners,
            spawnGenerator.leftSpawners,
            spawnGenerator.rightSpawners
        };

        List<GameObject> selectedSpawners = new List<GameObject>();

        if (numberOfDirection > allDirections.Count)
        {
            numberOfDirection = allDirections.Count; // Cap the number of directions to the available number
        }

        for (int i = 0; i < numberOfDirection; i++)
        {
            int randomIndex = Random.Range(0, allDirections.Count);
            selectedSpawners.AddRange(allDirections[randomIndex]);
            allDirections.RemoveAt(randomIndex); // Remove the selected direction to avoid repetition
        }

        return selectedSpawners;
    }

    private int CalculateNumberOfDirections(int wave)
    {
        if (wave % 10 == 0)
        {
            return 4;
        }
        else if (wave % 10 >= 7)
        {
            return 3;
        }
        else if (wave % 10 >= 4)
        {
            return 2;
        }
        else if (wave % 10 < 4)
        {
            return 1;
        }
        else 
        { 
            return 1; 
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
