using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireProjectile : MonoBehaviour
{

    [SerializeField] private GameAudioManager fireCrash;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D light2D;


    public float projectileSpeed = 10f;
    public float damage = 10f;
    public float projectileKnockback = 50f;

    public Rigidbody2D rb;
    public SpellOrbit spellOrbit;
    public FireSpell caster;
    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Shoots in the aiming direction
    public void Cast()
    {

        if (rb != null)
        {

            light2D.enabled = true;

            // Updates stats
            projectileSpeed = caster.projectileSpeed;
            damage = caster.damage;

            Vector2 shootDirection = new Vector2(Mathf.Cos(spellOrbit.currentAngle * Mathf.Deg2Rad), Mathf.Sin(spellOrbit.currentAngle * Mathf.Deg2Rad));
            rb.velocity = shootDirection * projectileSpeed; // Apply velocity in the direction of the current angle
        }
    }

    // Puts the spell back in the player's stack when it collides with something that is not the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().ApplyDamage(damage, true);

            if(collision.gameObject.GetComponent<KnockbackFeedback>() != null)

                collision.gameObject.GetComponent<KnockbackFeedback>().PlayFeedback(gameObject);

        }
        
        if (!collision.CompareTag("Player") && !collision.CompareTag("Room") && !collision.CompareTag("PlayerProjectile") && !collision.CompareTag("Orb"))
        {
            // Reset object state to rotation
            rb.velocity = Vector2.zero;
            fireCrash.PlayRandomSound();
            anim.Play("Crash");

        }
    }

    private IEnumerator ResetPool()
    {

        // Makes the object invisible and inactive
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        light2D.enabled = false;

        while (fireCrash.audioPlayed.isPlaying) 
        {
            yield return null;
        }

        gameObject.SetActive(false);

        // Add the spell back to the player's stack
        caster.spellStack.Push(gameObject);
    }

}
