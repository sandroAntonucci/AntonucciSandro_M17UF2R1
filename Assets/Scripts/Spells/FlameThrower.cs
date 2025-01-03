using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{

    [SerializeField] private FlameThrowerSpell spell;
    public float damage;

    void Start()
    {
        damage = spell.damage;
    }

    // Detects when the particles are triggering the enemy and applies damage
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().ApplyDamage(damage, false);
        }
    }

}
