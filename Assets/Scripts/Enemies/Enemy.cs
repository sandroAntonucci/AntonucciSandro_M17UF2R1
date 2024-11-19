using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    [SerializeField] public SimpleFlash damageFlash;
    [SerializeField] public ParticleSystem damageParticles;
    [SerializeField] public GameAudioManager damageSound;

    public List<PowerOrb> powerOrbs;

    public int rangeToDropOne;
    public int rangeToDropTwo;
    public int rangeToDropFive;

    public Transform player;
    public Rigidbody2D rb;

    public string type;
    public float damage;
    public float health;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;

        // Game object is active when the player enters the room
        gameObject.SetActive(false);

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
            DropPower();
            Destroy(gameObject);
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
}

