using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    [SerializeField] Animator animator;

    public ProjectileCaster caster;
    public Rigidbody2D rb;

    public float projectileSpeed;
    public float projectileDamage;

    private Transform playerPosition;
   

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Shoots in the given direction
    public void Shoot(Vector2 direction)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        direction = direction.normalized;
        rb.velocity = direction * projectileSpeed;
    }

    // Shoots in the players direction (used with ranged enemy)
    public void CastTowardsPlayer()
    {
        if (rb != null)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
            Vector2 shootDirection = new Vector2(playerPosition.position.x - transform.position.x, playerPosition.position.y - transform.position.y);
            rb.velocity = shootDirection.normalized * projectileSpeed; 

        }
    }

    // "Destroys" the projectile
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Room") || collision.CompareTag("PlayerProjectile")) return;

        rb.velocity = Vector2.zero;
        animator.Play("EnemyProjectileCrash");
    }

    // Resets the projectile to the caster pool
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
