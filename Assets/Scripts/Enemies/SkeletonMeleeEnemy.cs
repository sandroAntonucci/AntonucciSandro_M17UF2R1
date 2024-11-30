using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMeleeEnemy : Enemy
{

    [SerializeField] private float speed;

    private bool isFlipped = false;
    private bool isSpawned = false;

    private void Update()
    {

        if (!isSpawned) return;

        Move();
    }

    private void Move()
    {

        Vector2 direction = player.position - transform.position;
        rb.velocity = direction.normalized * speed;


        if (direction.x < 0 && !isFlipped)
        {
            FlipHorizontal();
        }
        else if (direction.x > 0 && isFlipped)
        {
            FlipHorizontal();
        }
    }

    private void FlipHorizontal()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        isFlipped = !isFlipped;
    }

    private void Spawn()
    {
        isSpawned = true;
    }


}
