using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    [SerializeField] public SimpleFlash damageFlash;
    [SerializeField] public ParticleSystem damageParticles;
    [SerializeField] public GameAudioManager damageSound;

    public Transform player;
    public Rigidbody2D rb;

    public string type;
    public float damage;
    public float health;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Applies damage to the enemy
    public void ApplyDamage(float damageApplied)
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
