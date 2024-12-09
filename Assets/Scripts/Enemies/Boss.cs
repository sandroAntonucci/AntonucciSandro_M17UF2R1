using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    [SerializeField] private GameObject itemDrop;

    public override void Start()
    {
        StartCoroutine(Spawn());
        base.Start();
    }

    private IEnumerator Spawn()
    {

        gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(true);

    }

    public override void Die()
    {

        CasterBoss casterBoss = GetComponent<CasterBoss>();

        // Destroys all projectiles when the boss dies
        if (casterBoss != null)
        {
            casterBoss.DestroyProjectiles();
        }

        itemDrop.SetActive(true);

        base.Die();

    }
}
