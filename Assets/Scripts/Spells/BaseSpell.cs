using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpell : MonoBehaviour
{

    // This is the base spell used for the player to shoot projectiles with any spell type
    public float attackSpeed;
    public float damage;
    public float projectileSpeed;

    public abstract void CastSpell();


}
