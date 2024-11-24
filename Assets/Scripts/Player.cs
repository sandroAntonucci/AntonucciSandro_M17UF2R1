using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{

    // Combat variables
    public float health = 100;
    public bool invincible = false;
    
    public PlayerControls playerControls;
    private InputAction fire;
    private bool isFiring;

    public BaseSpell spell;
    public PassiveSpell passiveSpell;
    public float invincibilityDuration = 1f;

    // "Coins" that the player can use to buy spells or modifications
    public int power;


    // Movement variables
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 currentVelocity;
    private float acceleration = 8f; 
    [SerializeField] float moveSpeed;

    // Animations, audio and effects
    Animator anim;
    private Vector2 lastMoveDirection;
    [SerializeField] private GameAudioManager hitAudio;
    [SerializeField] private SimpleFlash damageFlash;
    [SerializeField] private ParticleSystem damageParticles;

    // -- Input Actions --

    private void Awake()
    {
        playerControls = new PlayerControls();
        
    }

    private void OnEnable()
    {
        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.started += context => isFiring = true;
        fire.canceled += context => isFiring = false;
    }

    private void OnDisable()
    {
        fire.Disable();
    }

    // --------------------


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Animate();

        if (isFiring)
        {
            spell.GetComponent<BaseSpell>().CastSpell();
        }
    }

    void FixedUpdate()
    {
        // Interpolate between current velocity and target velocity
        currentVelocity = Vector2.Lerp(currentVelocity, moveInput * moveSpeed, acceleration * Time.fixedDeltaTime);
        rb.velocity = currentVelocity;
    }

    public void Move(InputAction.CallbackContext context)
    {

        // This is to set the right direction to an idle animation (detects if the player stopped input but was inputting last frame) 
        Vector2 moveInputCheck = context.ReadValue<Vector2>();

        if (moveInputCheck.x == 0 && moveInputCheck.y == 0 && (moveInput.x != 0 || moveInput.y != 0))
        {
            lastMoveDirection = moveInput;
        }

        moveInput = context.ReadValue<Vector2>();


    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        // Melee Enemies
        if (collision.gameObject.CompareTag("Enemy"))
        {


            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {

                hitAudio.PlayRandomSound();

                // Subtract the enemy's damage from the player's health
                TakeDamage(enemy.damage);

                // Check if health is 0 or less, and destroy the player if so
                if (health <= 0)
                {
                    gameObject.SetActive(false);
                }
            }

        }
        // Ranged enemies
        else if (collision.gameObject.CompareTag("EnemySpell"))
        {
            EnemyProjectile enemyProjectile = collision.gameObject.GetComponent<EnemyProjectile>();

            if (enemyProjectile != null)
            {
                hitAudio.PlayRandomSound();

                TakeDamage(enemyProjectile.damage);

                // Check if health is 0 or less, and destroy the player if so
                if (health <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private void TakeDamage(float enemyDamage)
    {
        if (invincible) return; // Skip if invincible

        health -= enemyDamage;
        StartCoroutine(HandleInvincibility());
        damageFlash.Flash();
        damageParticles.Play();
    }

    private IEnumerator HandleInvincibility()
    {
        invincible = true;

        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.enabled = false;

        yield return new WaitForSeconds(invincibilityDuration);

        collider.enabled = true;

        invincible = false;
    }

    void Animate()
    {
        anim.SetFloat("Horizontal", moveInput.x);
        anim.SetFloat("Vertical", moveInput.y);
        anim.SetFloat("MoveMagnitude", moveInput.magnitude);
        anim.SetFloat("lastMoveX", lastMoveDirection.x);
        anim.SetFloat("lastMoveY", lastMoveDirection.y);
    }

    public void AddPower(int powerQuant)
    {
        power += powerQuant;
    }

}
