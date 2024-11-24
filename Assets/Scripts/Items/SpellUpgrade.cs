using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellUpgrade : ItemUpgrade
{

    public override void Start()
    {
        base.Start();
        spell = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().spell;
    }

}
