using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    public SimpleFlash damageFlash;
    public ParticleSystem damageParticles;
    public GameAudioManager damageSound;

    // If the enemy is a boss minion, it will be active from the start
    [SerializeField] private bool isBossMinion = false;

    [SerializeField] protected FloatingHealthbar healthBar;

    public List<GameObject> powerOrbs;

    public int rangeToDropOne;
    public int rangeToDropTwo;
    public int rangeToDropFive;

    public Transform player;
    public Rigidbody2D rb;

    public string type;
    public float damage;
    public float health;

    private float maxHealth;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthbar>();
    }

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;

        maxHealth = health;

        // Game object is active when the player enters the room
        if(!isBossMinion) gameObject.SetActive(false);

    }

    // Applies damage to the enemy
    public void ApplyDamage(float damageApplied)
    {
        health -= damageApplied;

        damageSound.PlayRandomSound();
        damageFlash.Flash();
        damageParticles.Play();

        healthBar.UpdateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            Die();
        }
    } 

    public void DropPower()
    {

        int powerDrop = Random.Range(0, 100);

        if (powerDrop < rangeToDropFive)
        {
            Instantiate(powerOrbs[2], transform.position, Quaternion.identity);
        }
        else if (powerDrop < rangeToDropTwo)
        {
            Instantiate(powerOrbs[1], transform.position, Quaternion.identity);
        }
        else if (powerDrop < rangeToDropOne)
        {
            Instantiate(powerOrbs[0], transform.position, Quaternion.identity);
        }
    }

    public virtual void Die()
    {
        DropPower();
        Destroy(gameObject);
    }



}

