using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHorizontalMovement : MonoBehaviour
{
    public float speed = 1.0f; 
    public float distance = 5.0f; 

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Record the initial position to oscillate around this point
    }

    void Update()
    {
        // Calculate the new position as a function of time, creating a left-right oscillation
        transform.position = startPosition + Vector3.right * Mathf.Sin(Time.deltaTime * speed) * distance;
    }
}
