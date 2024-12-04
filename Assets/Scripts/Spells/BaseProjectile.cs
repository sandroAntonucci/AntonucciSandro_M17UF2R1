using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public ProjectileCaster caster;
    public Rigidbody2D rb;

    public float projectileSpeed;
    public float projectileDamage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 direction)
    {
        direction = direction.normalized;
        rb.velocity = direction * projectileSpeed; // Use Vector2 for 2D physics
    }
}
