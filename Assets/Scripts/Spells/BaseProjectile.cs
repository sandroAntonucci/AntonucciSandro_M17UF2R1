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
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        direction = direction.normalized;
        rb.velocity = direction * projectileSpeed; // Use Vector2 for 2D physics
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ResetPool();
    }

    private void ResetPool()
    {

        if (caster == null)
        {
            Destroy(gameObject);
        }

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.SetActive(false);
        caster.projectilePool.Push(gameObject);
    }

}
