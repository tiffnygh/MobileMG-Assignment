using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDetector : MonoBehaviour
{
    private CircleCollider2D barrierCollider;

    private void Awake()
    {
        barrierCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Calculate the distance from the enemy to the center of the barrier
        float distanceFromCenter = Vector3.Distance(transform.position, other.transform.position);

        // Only process the exit if the enemy is beyond the radius of the barrier
        if (distanceFromCenter > barrierCollider.radius)
        {
            Vector3 exitDirection = other.transform.position - transform.position;
            string segment = DetermineSegment(exitDirection);
            Debug.Log($"Enemy exited through the {segment} segment.");
        }
    }

    private string DetermineSegment(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;  // Normalize angle to be within 0-360 degrees

        if (angle > 45 && angle <= 135)
        {
            return "Top";
        }
        else if (angle > 135 && angle <= 225)
        {
            return "Left";
        }
        else if (angle > 225 && angle <= 315)
        {
            return "Bottom";
        }
        else
        {
            return "Right";
        }
    }
}
