using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSpell : BaseSpell
{

    [SerializeField] public Knife knife;

    public override void CastSpell()
    {
        anim.Play("MeleeAttack");
    }


}
