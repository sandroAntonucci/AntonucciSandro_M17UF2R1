using System.Collections;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public float dashForce = 10f;
    public float dashFriction = 2f;

    private Transform player;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Dash()
    {

        Vector2 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * dashForce, ForceMode2D.Impulse);
        rb.drag = dashFriction;

    }

}
