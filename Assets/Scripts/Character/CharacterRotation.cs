using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // Get the main camera in the scene
    }

    void Update()
    {
        RotateSpriteTowardsMouse();
    }

    void RotateSpriteTowardsMouse()
    {
        Vector3 mouseScreenPosition = Input.mousePosition; // Get the position of the mouse in screen coordinates
        mouseScreenPosition.z = mainCamera.nearClipPlane; // Set z to the near clip plane to ensure correct conversion
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition); // Convert to world position

        Vector3 direction = mouseWorldPosition - transform.position; // Calculate direction from sprite to mouse
        direction.z = 0; // Ensure no rotation around the z-axis if you are using 2D

        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calculate angle to rotate
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Apply rotation adjusting for sprite orientation
        }
    }
}
