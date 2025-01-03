using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSpell : BaseSpell
{

    [SerializeField] private Animator anim;
    [SerializeField] private BoxCollider2D hitbox;

    public override void CastSpell()
    {
        anim.Play("MeleeAttack");
    }

    // Doesn't do anything for melee spells
    public override void DestroyProjectiles()
    {
        return;
    }
}
