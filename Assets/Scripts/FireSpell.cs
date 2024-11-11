using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : MonoBehaviour
{

    public float projectileSpeed = 10f;

    public Player player;
    public Rigidbody2D rb;
    public SpellOrbit spellOrbit;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        spellOrbit = GetComponent<SpellOrbit>();
    }

    // Shoots in the aiming direction
    public void Shoot()
    {

        if (rb != null)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            Vector2 shootDirection = new Vector2(Mathf.Cos(spellOrbit.currentAngle * Mathf.Deg2Rad), Mathf.Sin(spellOrbit.currentAngle * Mathf.Deg2Rad));
            rb.velocity = shootDirection * projectileSpeed; // Apply velocity in the direction of the current angle

        }
    }

    // Puts the spell back in the player's stack when it collides with something that is not the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
        
        if (!collision.CompareTag("Player"))
        {

            gameObject.GetComponent<CircleCollider2D>().enabled = false;

            // Makes the object invisible and inactive
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.SetActive(false);

            // Reset object state to rotation
            rb.velocity = Vector2.zero;
            spellOrbit.isShooting = false; 

            // Pushes the object to the player's stack
            player.spellStack.Push(gameObject);
        }
    }

}
