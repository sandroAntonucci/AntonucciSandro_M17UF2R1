using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerSpell : BaseSpell
{

    [SerializeField] public ParticleSystem flameThrower;
    [SerializeField] public FlameThrower flameThrowerScript;    

    private bool isCasting = false;

    public override void CastSpell()
    {
        if (!isCasting)
        {
            isCasting = true;
            flameThrower.Play();
        }

    }

    public void StopSpell()
    {
        isCasting = false;
        flameThrower.Stop();    
    }

    public override void DestroyProjectiles()
    {
        return;
    }


}
