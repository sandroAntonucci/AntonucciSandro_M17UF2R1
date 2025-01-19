using UnityEngine;

public class FloatingMovement : MonoBehaviour
{
    // Amplitude of the floating effect
    public float amplitude = 0.5f;
    // Speed of the floating effect
    public float frequency = 1f;

    // Initial position of the object
    [SerializeField] private GameObject parentPosition;

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = parentPosition.transform.position + new Vector3(0, yOffset, 0);
    }

}

