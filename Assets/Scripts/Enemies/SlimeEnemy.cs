using System.Collections;
using UnityEngine;

public class SlimeEnemy : Enemy
{

    public float dashForce = 10f;
    public float dashFriction = 2f;

    private Transform player;
    private Rigidbody2D rb;

    [SerializeField] private SimpleFlash damageFlash;
    [SerializeField] private ParticleSystem damageParticles;
    [SerializeField] private GameAudioManager damageSound;
    [SerializeField] private AudioSource splashSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Dash()
    {
        splashSound.Play();
        Vector2 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * dashForce, ForceMode2D.Impulse);
        rb.drag = dashFriction;

    }

    public override void ApplyDamage(float damageApplied)
    {
        health -= damageApplied;

        damageSound.PlayRandomSound();
        damageFlash.Flash();
        damageParticles.Play();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
