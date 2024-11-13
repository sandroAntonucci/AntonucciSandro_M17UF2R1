using System.Collections;
using UnityEngine;

public class SlimeEnemy : Enemy
{

    public float dashForce = 10f;
    public float dashFriction = 2f;

    [SerializeField] private AudioSource splashSound;


    // Dashes in the player direction
    private void Dash()
    {
        splashSound.Play();
        Vector2 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * dashForce, ForceMode2D.Impulse);
        rb.drag = dashFriction;

    }

    

}
