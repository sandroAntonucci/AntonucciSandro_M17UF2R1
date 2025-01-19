using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSpellUpgrade : BaseUpgrade
{

    public override void UpgradeSpellDamage(float upgradeValue, string upgradeName, BaseSpell spell)
    {
        FlameThrowerSpell flameThrower = (FlameThrowerSpell)spell;
        flameThrower.flameThrowerScript.damage += upgradeValue / 20;
        
        flameThrower.damage += upgradeValue / 20;

        UpgradeCanvas.Instance.ShowCanvas(upgradeName, "Flame Damage +" + upgradeValue);
    }

    // Upgrades the size of the flame
    public override void UpgradeSpellAttackSpeed(float upgradeValue, string upgradeName, BaseSpell spell)
    {
        FlameThrowerSpell flameThrower = (FlameThrowerSpell)spell;

        flameThrower.flameThrower.startSize += upgradeValue * -2.5f;

        flameThrower.attackSpeed += upgradeValue;

        UpgradeCanvas.Instance.ShowCanvas(upgradeName, "Flame Scale +" + -upgradeValue);
    }

    // Upgrades the speed of the flame
    public override void UpgradeSpellProjectileSpeed(float upgradeValue, string upgradeName, BaseSpell spell)
    {
        FlameThrowerSpell flameThrower = (FlameThrowerSpell)spell;

        flameThrower.flameThrower.startSpeed += upgradeValue * 2.5f;

        flameThrower.projectileSpeed += upgradeValue;

        UpgradeCanvas.Instance.ShowCanvas(upgradeName, "Flame Projectile Speed +" + upgradeValue);
    }

}

