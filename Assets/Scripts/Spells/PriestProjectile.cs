using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    [SerializeField] private AudioSource hitAudio;

    public float projectileSpeed = 20f;
    public float damage;
    public Animator anim;

    public Rigidbody2D rb;
    public Transform playerPosition;
    public RangedEnemy caster;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Shoots in the players direction
    public void Cast()
    {

        if (rb != null)
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
            Vector2 shootDirection = new Vector2(playerPosition.position.x - transform.position.x, playerPosition.position.y - transform.position.y);
            rb.velocity = shootDirection.normalized * projectileSpeed; // Apply velocity in the direction of the player

        }
    }

    // Puts the projectile back in the priest stack when it collides with something that is not the player
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Enemy") || collision.CompareTag("Room") || collision.CompareTag("PlayerProjectile")) return;

        // Reset object state to rotation
        rb.velocity = Vector2.zero;
        hitAudio.Play();
        GetComponent<CircleCollider2D>().enabled = false;
        anim.Play("PriestProjectileCrash");

    }

    private void ResetPool()
    {
        // Makes the object invisible and inactive
        GetComponent<CircleCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.SetActive(false);

        // Add the spell back to the player's stack
        caster.projectileStack.Push(gameObject);
    }

}
