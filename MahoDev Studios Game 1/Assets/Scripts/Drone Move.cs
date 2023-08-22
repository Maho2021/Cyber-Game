using UnityEngine;

public class FlyBackAndForth : MonoBehaviour
{
    public float speed = 5.0f; // Adjust this to control the speed of movement

    private Vector3 originalPosition;

    void Start()
    {
        // Store the original position of the GameObject
        originalPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new X position using the PingPong function
        float newX = originalPosition.x + Mathf.PingPong(Time.time * speed, 2.0f); // Change 10.0f to adjust the width

        // Create the new position with the updated X coordinate
        Vector3 newPosition = new Vector3(newX, originalPosition.y, originalPosition.z);

        // Update the GameObject's position
        transform.position = newPosition;
    }
}