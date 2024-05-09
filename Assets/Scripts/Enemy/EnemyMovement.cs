using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EnemyBase
{
    private float currentSpeed;
    private float currentAcceleration;

    [SerializeField] private float sineWaveFrequency = 2.0f;  // Frequency of the wave
    [SerializeField] private float sineWaveMagnitude = 0.5f;  // Magnitude of the wave

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        currentSpeed = speed; // Initialize with the base speed
        currentAcceleration = acceleration;
    }

    // Update is called once per frame
    private void Update()
    {
        if (canMove)
        {
            MoveEnemy();
        }
    }

    private void MoveEnemy()
    {
        forwardMovement = Direction * (currentSpeed / 10f) * Time.fixedDeltaTime;

        horizontalMovement = Vector2.right * Mathf.Sin(Time.time * sineWaveFrequency) * sineWaveMagnitude;
        movement = forwardMovement + new Vector2(horizontalMovement.x, 0);
        myRigidbody2D.MovePosition(myRigidbody2D.position + movement);

        //movement = forwardMovement;
        //myRigidbody2D.MovePosition(myRigidbody2D.position + movement);

        currentSpeed += currentAcceleration * Time.deltaTime;
    }
}
