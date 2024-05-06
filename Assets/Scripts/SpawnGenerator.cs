using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour
{
    [SerializeField] private float zeroThreshold = 0.001f;  // Values below this threshold are treated as zero

    [SerializeField] private int numberOfSpawnPoint = 16;
    [SerializeField] private float radius = 5.0f;  // Radius from the player
    [SerializeField] private List<Vector3> positions = new List<Vector3>();
    [SerializeField] public List<GameObject> allSpawners = new List<GameObject>();

    [SerializeField] private GameObject player;  // Reference to the player GameObject

    [SerializeField] public List<GameObject> topSpawners = new List<GameObject>();
    [SerializeField] public List<GameObject> downSpawners = new List<GameObject>();
    [SerializeField] public List<GameObject> leftSpawners = new List<GameObject>();
    [SerializeField] public List<GameObject> rightSpawners = new List<GameObject>();

    void Start()
    {
        CalculatePositions();
        PositionObjects();
    }

    void CalculatePositions()
    {
        allSpawners.Clear();
        float angleStep = 360.0f / numberOfSpawnPoint;  // Divide the circle into 16 steps
        for (int i = 0; i < numberOfSpawnPoint; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;  // Convert degrees to radians
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            // Apply the zero threshold
            x = Mathf.Abs(x) < zeroThreshold ? 0 : x;
            y = Mathf.Abs(y) < zeroThreshold ? 0 : y;

            Vector3 pos = new Vector3(x, y, 0);
            positions.Add(pos);
        }
    }

    void PositionObjects()
    {
        foreach (Vector3 pos in positions)
        {
            GameObject obj = new GameObject("EnemySpawnPoint");  // Create a new GameObject for clarity
            obj.transform.position = player.transform.position + pos;  // Position it relative to the player
            obj.transform.parent = this.transform;  // Optionally parent it under this object for organization
            
            if (pos.y > 0)
            {
                topSpawners.Add(obj);

            }
            else if (pos.y < 0)
            {
                downSpawners.Add(obj);
            }

            if (pos.x > 0)
            {
                rightSpawners.Add(obj);
            }
            else if (pos.x < 0)
            {
                leftSpawners.Add(obj);
            }

            allSpawners.Add(obj);
        }
    }

    private void OnDrawGizmos()
    {
        foreach (Vector3 pos in positions)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pos, 0.1f);
        }
    }
}
