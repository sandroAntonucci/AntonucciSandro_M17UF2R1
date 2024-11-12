using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : BaseSpell
{

    public GameObject fireProjectile;
    public Stack<GameObject> spellStack;
    private bool canCast = true;

    public  void Start()
    {
        spellStack = new Stack<GameObject>();
    }

    public override void CastSpell()
    {

        if (!canCast) return;

        if (spellStack.Count > 0)
        {

            GameObject spellProjectile = spellStack.Pop();

            // Resets projectile position, sets it to active and shoots
            spellProjectile.transform.position = transform.position;
            spellProjectile.SetActive(true);
            spellProjectile.GetComponent<SpriteRenderer>().enabled = true;
            spellProjectile.GetComponent<FireProjectile>().Cast();

        }
        else
        {

            // Instantiates a new projectile and shoots
            GameObject spellProjectile = Instantiate(fireProjectile, transform.position, Quaternion.identity);
            spellProjectile.GetComponent<FireProjectile>().caster = this;
            spellProjectile.GetComponent<FireProjectile>().spellOrbit = GetComponent<SpellOrbit>();
            spellProjectile.GetComponent<FireProjectile>().Cast();

        }

        StartCoroutine(SpellCooldown());

    }

    // Sets a cast cooldown
    public IEnumerator SpellCooldown()
    {
        canCast = false;
        yield return new WaitForSeconds(attackSpeed);
        canCast = true;
    }


}
