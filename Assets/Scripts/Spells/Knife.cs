using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{


    [SerializeField] private MeleeSpell spell;
    public float damage;

    void Start()
    {
        damage = spell.damage;
    }

    // Puts the spell back in the player's stack when it collides with something that is not the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().ApplyDamage(damage, true);

            if (collision.gameObject.GetComponent<KnockbackFeedback>() != null)

                collision.gameObject.GetComponent<KnockbackFeedback>().PlayFeedback(gameObject);

        }

    }

}
