using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveUpgrade : ItemUpgrade
{

    public override void Start()
    {
        base.Start();

        // Gets the passive spell from the player
        spell = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().passiveSpell;
    }
    

}
