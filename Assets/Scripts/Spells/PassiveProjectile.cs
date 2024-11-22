using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PassiveProjectile : MonoBehaviour
{

    public float projectileSpeed = 10f;
    public float damage = 10f;

    public PassiveSpell caster;
    public Rigidbody2D rb;
    public Animator anim;


    // Shoots in the enemy position direction
    public void Cast(Vector3 enemyPosition)
    {

        

        // Makes the object visible and active
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.SetActive(true);
        anim.Play("PassiveSpellIdle");

        // Calculate the direction of the projectile
        Vector3 direction = enemyPosition - transform.position;
        direction.Normalize();

        // Shoot the projectile
        rb.velocity = direction * projectileSpeed;

    }

    // Puts the spell back in the player's stack when it collides with something that is not the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().ApplyDamage(damage);
        }

        if (!collision.CompareTag("Player") && !collision.CompareTag("Room")  && !collision.CompareTag("EnemySpell"))
        {
            rb.velocity = Vector2.zero;
            anim.Play("PassiveSpellCrash");
        }
    }

    private void ResetPool()
    {
        // Makes the object invisible and inactive
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.SetActive(false);

        // Add the spell back to the player's stack
        caster.spellStack.Push(gameObject);
    }
}
