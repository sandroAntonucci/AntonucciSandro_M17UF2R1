using UnityEngine;

public class FloatingMovement : MonoBehaviour
{
    // Amplitude of the floating effect
    public float amplitude = 0.5f;
    // Speed of the floating effect
    public float frequency = 1f;

    // Initial position of the object
    private Vector3 startPosition;

    void Start()
    {
        // Store the initial position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new position
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPosition + new Vector3(0, yOffset, 0);
    }
}

