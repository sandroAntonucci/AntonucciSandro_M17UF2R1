using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    [SerializeField] private GameObject itemDrop;
    [SerializeField] private GameObject pedestal;

    public override void Die()
    {

        CasterComponent casterComponent = GetComponent<CasterComponent>();

        // Destroys all projectiles when the boss dies
        if (casterComponent != null)
        {
            casterComponent.DestroyProjectiles();
        }

        if (itemDrop != null) itemDrop.SetActive(true);
        if (pedestal != null) pedestal.SetActive(true);

        base.Die();

    }
}
