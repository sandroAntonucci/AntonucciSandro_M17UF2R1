using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private float distanceToPlayer;

    private bool isFlipped = false;
    private bool isSpawned = false;

    private void Update()
    {
        if (!isSpawned || isDying) return;

        Move();
        FlipHorizontally();
    }

    private void Spawn()
    {
        enemyCollider.enabled = true;
        isSpawned = true;
        projectileCaster.enabled = true;
    }

    // Moves towards the player and stops between a certain distance
    private void Move()
    {

        // Check if the enemy is far from the target
        if (Vector2.Distance(transform.position, player.position) > distanceToPlayer)
        {
            projectileCaster.isShooting = false;
            Vector2 moveDirection = (player.position - transform.position).normalized;
            rb.velocity = moveDirection * 2f;
        }
        else
        {
            projectileCaster.isShooting = true;
            rb.velocity = Vector2.zero; 
        }
    }

    // Flips the player horizontally
    private void FlipHorizontally()
    {

        if (player.position.x < transform.position.x && !isFlipped)
        {
            isFlipped = true;
            transform.localScale = new Vector3(-1, 1, 1);

            // Doesn't change healthbar
            healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x * -1, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        }
        else if (player.position.x > transform.position.x && isFlipped)
        {
            isFlipped = false;
            transform.localScale = new Vector3(1, 1, 1);

            // Doesn't change healthbar
            healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x * -1, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        }

    }


}



