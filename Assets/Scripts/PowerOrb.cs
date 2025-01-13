using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PowerOrb : MonoBehaviour
{

    public int powerQuant;

    public bool isHealthOrb;

    public Collider2D dropCollider;

    public FloatingMovement floatingMovement;
    public GameAudioManager gameAudioManager;

    public Vector2 playerPosition;

    public bool playerInRange = false;

    public GameObject pickupEffect;
    public GameObject dropSprite;

    public void FixedUpdate()
    {
        if (playerInRange)
        {

            if (GameObject.FindGameObjectWithTag("Player") == null) return;
            if (floatingMovement == null) return;

            floatingMovement.enabled = false;
            playerPosition = GameObject.FindWithTag("Player").transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, 0.2f);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (isHealthOrb)
            {
                collision.collider.GetComponent<Player>().AddHealth(powerQuant);
            }
            else
            {
                collision.collider.GetComponent<Player>().AddPower(powerQuant);
            }

            dropCollider.enabled = false;
            Destroy(dropSprite);
            gameAudioManager.PlayRandomSound();
            pickupEffect.SetActive(true);
            Destroy(gameObject, 0.5f);
        }
    }

}

