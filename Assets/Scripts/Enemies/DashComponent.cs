using System.Collections;
using UnityEngine;

public class DashComponent : MonoBehaviour
{

    public float dashForce = 10f;
    public float dashFriction = 2f;

    public bool canDash = true;

    private Rigidbody2D rb;
    private Transform player;

    [SerializeField] private AudioSource splashSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }


    // Dashes in the player direction
    private void Dash()
    { 
        if (!canDash) return;

        splashSound.Play();
        Vector2 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * dashForce, ForceMode2D.Impulse);
        rb.drag = dashFriction;
    }

}
