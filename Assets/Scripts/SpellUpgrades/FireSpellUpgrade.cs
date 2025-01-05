using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpellUpgrade : BaseUpgrade
{

    public override void UpgradeSpellDamage(float upgradeValue, string upgradeName, BaseSpell spell)
    {
        spell.damage += upgradeValue;
        UpgradeCanvas.Instance.ShowCanvas(upgradeName, "Spell Damage +" + upgradeValue);
    }

    public override void UpgradeSpellAttackSpeed(float upgradeValue, string upgradeName, BaseSpell spell)
    {
        spell.attackSpeed += upgradeValue;
        UpgradeCanvas.Instance.ShowCanvas(upgradeName, "Spell Attack Speed " + upgradeValue);
    }

    public override void UpgradeSpellProjectileSpeed(float upgradeValue, string upgradeName, BaseSpell spell)
    {
        spell.projectileSpeed += upgradeValue;
        UpgradeCanvas.Instance.ShowCanvas(upgradeName, "Spell Projectile Speed +" + upgradeValue);
    }

}
