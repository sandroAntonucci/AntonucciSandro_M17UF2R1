using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private float distanceToPlayer;

    public Stack<GameObject> projectileStack;
    public GameObject projectile;
    public Transform shootPosition;

    private bool isFlipped = false;
    private bool isSpawned = false;

    public override void Start()
    {
        base.Start();
        projectileStack = new Stack<GameObject>();
    }

    private void Update()
    {
        if (!isSpawned) return;

        Move();
        FlipHorizontally();
    }

    private void Spawn()
    {
        isSpawned = true;
    }

    // Is called in the animator
    private void Shoot()
    {
        if (projectileStack.Count > 0)
        {

            GameObject currentProjectile = projectileStack.Pop();

            // Resets projectile position, sets it to active and shoots
            currentProjectile.transform.position = shootPosition.position;
            currentProjectile.SetActive(true);
            currentProjectile.GetComponent<SpriteRenderer>().enabled = true;
            currentProjectile.GetComponent<EnemyProjectile>().Cast();

        }
        else
        {

            // Instantiates a new projectile and shoots
            GameObject currentProjectile = Instantiate(projectile, shootPosition.position, Quaternion.identity);
            currentProjectile.GetComponent<EnemyProjectile>().caster = this;
            currentProjectile.GetComponent<EnemyProjectile>().damage = damage;
            currentProjectile.GetComponent<EnemyProjectile>().Cast();

        }
    }

    // Moves towards the player and stops between a certain distance
    private void Move()
    {

        if (Vector2.Distance(transform.position, player.position) > distanceToPlayer)
        {
            Vector2 moveDirection = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
            rb.velocity = moveDirection.normalized * 2f;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }

    private void FlipHorizontally()
    {

        if (player.position.x < transform.position.x && !isFlipped)
        {
            isFlipped = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.position.x > transform.position.x && isFlipped)
        {
            isFlipped = false;
            transform.localScale = new Vector3(1, 1, 1);
        }

    }


}



