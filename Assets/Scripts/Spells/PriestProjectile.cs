using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestProjectile : MonoBehaviour
{

    [SerializeField] private AudioSource hitAudio;
    [SerializeField] private GameAudioManager audioManager;

    public float projectileSpeed = 10f;
    public float damage = 10f;

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
            audioManager.PlayRandomSound();
            Vector2 shootDirection = new Vector2(Mathf.Cos(spellOrbit.currentAngle * Mathf.Deg2Rad), Mathf.Sin(spellOrbit.currentAngle * Mathf.Deg2Rad));
            rb.velocity = shootDirection * projectileSpeed; // Apply velocity in the direction of the current angle

        }
    }

    // Puts the spell back in the player's stack when it collides with something that is not the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().ApplyDamage(damage);
        }
        
        if (!collision.CompareTag("Player"))
        {
            // Reset object state to rotation
            rb.velocity = Vector2.zero;
            anim.Play("Crash");
            hitAudio.PlayOneShot(hitAudio.clip, 1f);

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
