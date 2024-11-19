using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSpell : BaseSpell
{
    public GameObject passiveProjectile;
    public Stack<GameObject> spellStack;

    public float range = 3f;

    private bool canCast = true;

    public void Start()
    {
        spellStack = new Stack<GameObject>();
    }

    public void Update()
    {

        // Shoots when there is an enemy in range
        if(FindClosestEnemy() != null)
        {
            CastSpell();
        }
    }

    public override void CastSpell()
    {

        if (!canCast) return;

        // Find the closest enemy within range
        GameObject closestEnemy = FindClosestEnemy();

        Vector3 targetPosition = closestEnemy.transform.position;

        if (spellStack.Count > 0)
        {
            GameObject spellProjectile = spellStack.Pop();

            // Resets projectile position and shoots toward the enemy
            spellProjectile.transform.position = transform.position;
            spellProjectile.GetComponent<PassiveProjectile>().Cast(targetPosition);
        }
        else
        {
            // Instantiate a new projectile and shoot toward the enemy
            GameObject spellProjectile = Instantiate(passiveProjectile, transform.position, Quaternion.identity);

            PassiveProjectile passiveProjectileInstance = spellProjectile.GetComponent<PassiveProjectile>();

            passiveProjectileInstance.damage = damage;
            passiveProjectileInstance.projectileSpeed = projectileSpeed;
            passiveProjectileInstance.caster = this;
            passiveProjectileInstance.Cast(targetPosition);
        }

        StartCoroutine(SpellCooldown());

    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = range;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    public IEnumerator SpellCooldown()
    {
        canCast = false;
        yield return new WaitForSeconds(attackSpeed);
        canCast = true;
    }

}


