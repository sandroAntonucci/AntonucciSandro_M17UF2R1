using UnityEngine;
using UnityEngine.InputSystem;

public class FireOrbit : MonoBehaviour
{
    [SerializeField] private Transform player;            // Reference to the player (center point)
    [SerializeField] private float orbitRadius = 1f;      // Distance from the player
    [SerializeField] private float rotationSpeed = 5f;    // Speed of the rotation
    [SerializeField] private float projectileSpeed = 10f; // Speed at which the projectile moves
    [SerializeField] private Rigidbody2D rb;              // Reference to Rigidbody2D of the fire object (projectile)

    private Camera mainCamera;
    private Vector2 lookInput;        // Input from the mouse or the right stick on the controller
    private float targetAngle;        // The target angle to rotate towards
    private float currentAngle;       // The current angle of the fire object
    private bool isShooting = false;  // A flag to check if the projectile is shot

    private void Start()
    {
        mainCamera = Camera.main;
        currentAngle = 0f; // Start the object at the initial angle
    }

    private void Update()
    {
        // Get look input from either the mouse or the right stick on the controller
        lookInput = GetLookInput();

        // If we are not shooting, continue rotating the object around the player
        if (!isShooting)
        {
            RotateAroundPlayer();
        }
        else
        {
            Shoot();
        }
    }

    private Vector2 GetLookInput()
    {
        // Check if a gamepad is connected
        if (Gamepad.current != null)
        {
            // If a controller is connected, use the right stick input
            Vector2 rightStick = Gamepad.current.rightStick.ReadValue();
            return rightStick.normalized; // Get normalized direction from the right stick
        }
        else if (Mouse.current != null)
        {
            // If no controller is connected, use the mouse position to calculate direction
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePosition.z = 0; // Keep the position on the same plane
            return (mousePosition - player.position).normalized; // Get direction from player to mouse
        }

        // Default case, return Vector2.zero if no input
        return Vector2.zero;
    }

    private void RotateAroundPlayer()
    {
        // If we have valid input (not zero)
        if (lookInput != Vector2.zero)
        {
            // Calculate the angle from the player to the target direction (either mouse or right stick)
            targetAngle = Mathf.Atan2(lookInput.y, lookInput.x) * Mathf.Rad2Deg; // Convert to degrees
        }

        // Smoothly interpolate the current angle towards the target angle
        currentAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        // Calculate the new position based on the current angle and orbit radius
        float radianAngle = currentAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle), 0) * orbitRadius;

        // Set the object's position at the new location, keeping it at the orbitRadius distance 
        transform.position = player.position + offset;
    }

    // Called to start shooting
    public void Shoot()
    {

        Debug.Log("ola");

        isShooting = true;

        if (rb != null)
        {
            // Calculate the direction to shoot in (based on the current angle)
            Vector2 shootDirection = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

            // Apply velocity to the Rigidbody2D to move the object in the shoot direction
            rb.velocity = shootDirection * projectileSpeed; // Apply velocity in the direction of the current angle

        }
    }
}
