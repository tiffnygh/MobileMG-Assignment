using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour
{
    [SerializeField] private int numberOfSpawnPoint = 16;
    [SerializeField] private float radius = 5.0f;  // Radius from the player
    [SerializeField] private List<Vector3> positions = new List<Vector3>();
    [SerializeField] private GameObject player;  // Reference to the player GameObject

    void Start()
    {
        CalculatePositions();
        PositionObjects();
    }

    void CalculatePositions()
    {
        positions.Clear();
        float angleStep = 360.0f / numberOfSpawnPoint;  // Divide the circle into 16 steps
        for (int i = 0; i < numberOfSpawnPoint; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;  // Convert degrees to radians
            Vector3 pos = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
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
