using UnityEngine;

public class FlyingCar : MonoBehaviour
{
    public float moveSpeed = 5.0f;  // Speed of movement
    public float rotationSpeed = 30.0f;  // Speed of rotation

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private void Start()
    {
        // Initial target position and rotation
        targetPosition = transform.position;
        targetRotation = transform.rotation;
    }

    private void Update()
    {
        // Move the car towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Rotate the car towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // If the car has reached the target position, generate a new random target
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            GenerateRandomTarget();
        }
    }

    private void GenerateRandomTarget()
    {
        // Generate a new random target position within a certain range
        float range = 10.0f;
        targetPosition = new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range)) + transform.position;

        // Generate a new random target rotation
        targetRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }
}
