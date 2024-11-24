using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : BaseSpell
{

    
    public Stack<GameObject> spellStack;

    public GameAudioManager castSound;

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

            // Resets projectile position, updates stats, sets it to active and shoots
            spellProjectile.transform.position = transform.position;
            spellProjectile.SetActive(true);
            spellProjectile.GetComponent<SpriteRenderer>().enabled = true;
            spellProjectile.GetComponent<FireProjectile>().Cast();

        }
        else
        {

            // Instantiates a new projectile and shoots
            GameObject spellProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

            FireProjectile fireProjectileInstance = spellProjectile.GetComponent<FireProjectile>();

            // Assigns stats and shoots
            fireProjectileInstance.caster = this;
            fireProjectileInstance.spellOrbit = GetComponent<SpellOrbit>();
            fireProjectileInstance.Cast();


        }

        castSound.PlayRandomSound();

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
