using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EnemyBase
{
    private float currentSpeed;
    private float currentAcceleration;

    private float elapsedTime = 0; // Time elapsed for oscillation calculation


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
        if (canForward)
        {
            forwardMovement = Direction * (currentSpeed / 10f) * Time.fixedDeltaTime;
        }

        //horizontalMovement = Direction.normalized * Mathf.Sin(horizontalAmount) * horizontalDistance;
        if (canHorizontal)
        {
            elapsedTime += Time.fixedDeltaTime;
            float horizontalAmount = Mathf.Sin(elapsedTime * horizontalSpeed) * horizontalDistance;
            Vector2 rightVector = new Vector2(-Direction.y, Direction.x).normalized; // Perpendicular to the main direction
            horizontalMovement = rightVector * horizontalAmount;
        }

        if (canZigZag)
        {
            if (Time.time > nextChangeTime)
            {
                Direction.x *= -1; // Change horizontal direction
                nextChangeTime = Time.time + zigzagChangeTime;
            }
            zigzagMovemnet = Direction * (currentSpeed / 10f) * Time.fixedDeltaTime;
        }

        if (canSpiral)
        {
            StartSpiral();

            // Update the spiral angle and radius
            spiralAngle += spiralSpeed * Time.deltaTime;
            if (negativeSpiral)
            {
                spiralRadius -= spiralExpansionRate * Time.deltaTime;
            }
            else
            {
                spiralRadius += spiralExpansionRate * Time.deltaTime;
            }

            // Calculate the new position in the spiral
            Vector2 spiralOffset = new Vector2(Mathf.Cos(spiralAngle * Mathf.Deg2Rad), Mathf.Sin(spiralAngle * Mathf.Deg2Rad)) * spiralRadius;
            Vector2 nextPosition = (Vector2)playerTransform.position + spiralOffset;

            spiralMovement = nextPosition - (Vector2)transform.position;

            // Normalize and scale the movement by the current speed and deltaTime
            spiralMovement = spiralMovement.normalized * (currentSpeed * Time.deltaTime);
        }


        //Sum of movement 
        movement = forwardMovement + horizontalMovement + zigzagMovemnet + spiralMovement;
        myRigidbody2D.MovePosition(myRigidbody2D.position + movement);

        currentSpeed += currentAcceleration * Time.deltaTime;
    }
}
