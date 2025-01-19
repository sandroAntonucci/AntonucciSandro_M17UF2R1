using UnityEngine;

public class CharacterSlide : MonoBehaviour
{
    public float slidingFactor = 0.9f; // Adjust this to control the amount of sliding (0 to 1)
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Ensure the character slides upon collision
        Vector3 incomingVelocity = rb.velocity;

        // Calculate the sliding direction (parallel to the surface)
        Vector3 normal = collision.contacts[0].normal;
        Vector3 slideDirection = Vector3.ProjectOnPlane(incomingVelocity, normal);

        // Reduce the velocity to simulate sliding
        rb.velocity = slideDirection * slidingFactor;
    }

    void OnCollisionStay(Collision collision)
    {
        // Continuously apply sliding effect while in contact
        OnCollisionEnter(collision);
    }
}
