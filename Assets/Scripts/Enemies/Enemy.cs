using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    public string type;
    public float damage;
    public float health;

    public abstract void ApplyDamage(float damageApplied);

}
