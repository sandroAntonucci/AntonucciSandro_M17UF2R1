using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{

    // Combat variables
    public int health = 100;
    public bool invincible = false;
    public FireOrbit spell;
    public PlayerControls playerControls;
    private InputAction fire;
    
    // Movement variables
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 currentVelocity;
    private float acceleration = 8f; 
    [SerializeField] float moveSpeed;

    // Animations and effects
    Animator anim;
    private Vector2 lastMoveDirection;
    [SerializeField] private SimpleFlash damageFlash;
    [SerializeField] private ParticleSystem damageParticles;

    private void Awake()
    {
        playerControls = new PlayerControls();
        
    }

    private void OnEnable()
    {
        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable()
    {
        fire.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

    // Right trigger attack

    public void Fire(InputAction.CallbackContext context)
    {
        spell.Shoot();
    }





    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
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


    private void TakeDamage(int enemyDamage)
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
        yield return new WaitForSeconds(damageFlash.GetFlashDuration());
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


}
