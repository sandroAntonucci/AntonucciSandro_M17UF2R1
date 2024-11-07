using System.Collections;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public float dashForce = 10f;
    public float dashCooldown = 1f;
    public float dashDuration = 0.2f;
    public float dashFriction = 2f;

    private Transform player;
    private Rigidbody2D rb;
    private float dashTimer;
    private bool isDashing;
    private bool isSpawned;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (!isSpawned) return;

        dashTimer -= Time.deltaTime;

        if (!isDashing && dashTimer <= 0)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * dashForce, ForceMode2D.Impulse);
        rb.drag = dashFriction;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        dashTimer = dashCooldown;
        rb.drag = 0.5f;
    }

    public void Spawn()
    {
        animator.SetBool("isSpawned", true);
        isSpawned = true;
    }
}
