using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;

public class Player : MonoBehaviour
{

    public static Player Instance { get; private set; }

    // Combat variables
    public float health = 50;
    public float maxHealth = 50;
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
    public Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 currentVelocity;
    private float acceleration = 8f;

    private float moveSpeed;

    [SerializeField] float baseMoveSpeed;

    // Animations, audio and effects
    Animator anim;
    private Vector2 lastMoveDirection;
    [SerializeField] private GameAudioManager hitAudio;
    [SerializeField] private SimpleFlash damageFlash;
    [SerializeField] private ParticleSystem damageParticles;

    // Signals 
    public static event Action<int> OnPowerAdded;
    public static event Action<int> OnHealthChanged;
    public static event Action<int> OnCurrentHealthChanged;

    // -- Input Actions --

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += context => StartShooting();
        fire.canceled += context => StopShooting();
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

        // If the player is loaded it means that all referenced scenes are loaded in the main scene, so we can remove them 
        StartCoroutine(GameManager.Instance.RemoveScenes());

        moveSpeed = baseMoveSpeed;

        OnHealthChanged?.Invoke((int)health / 10);
        OnCurrentHealthChanged?.Invoke((int)health / 10);
    }

    // This is done because you can't start a coroutine from context in player controls
    private void StartShooting()
    {
        if(isFiring) return;
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        isFiring = true;

        while (isFiring) 
        {
            spell.GetComponent<BaseSpell>().CastSpell(); 

            yield return null; 
        }

        moveSpeed = baseMoveSpeed;
    }

    private void StopShooting()
    {
        isFiring = false; // This will exit the while loop in the Shoot coroutine
    }

    

    private void Update()
    {
        Animate();
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

    // Handles collision with melee enemies
    public void OnTriggerStay2D(Collider2D collision)
    {

        if (invincible) return;


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

    }

    // Handles collision with ranged enemies (projectiles)
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (invincible) return;

        if (collision.gameObject.CompareTag("EnemySpell"))
        {
            EnemyProjectile enemyProjectile = collision.gameObject.GetComponent<EnemyProjectile>();

            if (enemyProjectile != null)
            {
                hitAudio.PlayRandomSound();

                TakeDamage(enemyProjectile.projectileDamage);

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
        health -= enemyDamage;
        OnCurrentHealthChanged?.Invoke((int)health / 10);
        StartCoroutine(HandleInvincibility());
        damageFlash.Flash();
        damageParticles.Play();
    }

    private IEnumerator HandleInvincibility()
    {
        invincible = true;


        yield return new WaitForSeconds(invincibilityDuration);


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
        OnPowerAdded?.Invoke(power);
    }

    // Used in item buying
    public void EmitPowerAdded()
    {
        OnPowerAdded?.Invoke(power);
    }

    public void DestroyProjectiles()
    {
        spell.DestroyProjectiles();
        passiveSpell.DestroyProjectiles();
    }

    public void ReloadPlayer()
    {
        DestroyProjectiles();
        gameObject.GetComponent<PlayerSpawn>().EnablePlayer();
        lastMoveDirection = Vector2.zero;
        gameObject.transform.position = new Vector3(0.0001f, 0, 0);
    }

    public void AddHealth(int healthQuant)
    {

        if (health + healthQuant > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += healthQuant;
        }

        OnCurrentHealthChanged?.Invoke((int)health / 10);
    }

}
