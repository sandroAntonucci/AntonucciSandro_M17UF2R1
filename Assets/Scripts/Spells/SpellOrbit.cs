using UnityEngine;
using UnityEngine.InputSystem;

public class SpellOrbit : MonoBehaviour
{
    [SerializeField] private float orbitRadius = 1f;      // Distance from the player
    [SerializeField] private float rotationSpeed = 5f;    // Speed of the rotation

    private Camera mainCamera;
    private Vector2 lookInput;        // Input from the mouse or the right stick on the controller
    public float targetAngle;        // The target angle to rotate towards
    public float currentAngle;       // The current angle of the fire object
    public Transform player;            // Reference to the player (center point)

    public bool followsInput = true;
    public bool rotatesObject = false;


    private void Start()
    {
        transform.position = player.position +  new Vector3(orbitRadius, 0f, 0f);
        player = GameObject.FindWithTag("Player").transform;
        mainCamera = Camera.main;
    }

    private void Update()
    {

        if (followsInput && !rotatesObject)
        {
            lookInput = GetLookInput();

            RotateAroundPlayer();
        }
        else if (followsInput && rotatesObject)
        {
            lookInput = GetLookInput();

            RotateAndOrientAroundPlayer();
        }
        else
        {
            RotateAroundPlayerWithoutInput();
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

    private void RotateAndOrientAroundPlayer()
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

        // Rotate the object to face outward from the center (player)
        Vector3 directionToPlayer = (player.position - transform.position).normalized; // Direction pointing to the center
        float rotationAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg + 90f; // Adjust angle to align with object's forward direction
        transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
    }

    private void RotateAroundPlayerWithoutInput()
    {
        // Update the current angle based on the rotation speed
        currentAngle += -rotationSpeed * Time.deltaTime;
        currentAngle %= 360; // Keep the angle within 0-360 degrees

        // Calculate the new position based on the current angle and orbit radius
        float radianAngle = currentAngle * Mathf.Deg2Rad; // Convert degrees to radians
        Vector3 offset = new Vector3(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle), 0) * orbitRadius;

        // Set the object's position at the new location, keeping it at the orbitRadius distance 
        transform.position = player.position + offset;
    }

}
