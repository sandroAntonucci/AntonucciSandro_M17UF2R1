using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpell : MonoBehaviour
{

    // This is the base spell used for the player to shoot projectiles with any spell type
    public float attackSpeed;
    public float damage;
    public float projectileSpeed;
    public float range;

    public GameObject projectile;

    public abstract void CastSpell();

    public abstract void DestroyProjectiles();

    public abstract void UpgradeSpellDamage(float upgradeValue);

    public abstract void UpgradeSpellAttackSpeed(float upgradeValue);

    public abstract void UpgradeSpellProjectileSpeed(float upgradeValue);

    public abstract void UpgradeSpellRange(float upgradeValue);

}
