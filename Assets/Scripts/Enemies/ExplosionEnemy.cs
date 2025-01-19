using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExplosionEnemy : MeleeEnemy
{
    [SerializeField] private BoxCollider2D boxCollider;

    public override void Spawn()
    {
        Debug.Log("Spawned");
        boxCollider.enabled = true;
        isSpawned = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boxCollider.enabled = false;
            healthBar.gameObject.SetActive(false);
            rb.velocity = Vector2.zero;
            isDying = true;
            anim.Play("Death");
        }
    }

}
