using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveUpgrade : BaseUpgrade
{

    public override void UpgradeSpellDamage(float upgradeValue, string upgradeName, BaseSpell spell)
    {
        spell.damage += upgradeValue;
        UpgradeCanvas.Instance.ShowCanvas(upgradeName, "Passive Damage +" + upgradeValue);
    }

    public override void UpgradeSpellAttackSpeed(float upgradeValue, string upgradeName, BaseSpell spell)
    {
        spell.attackSpeed += upgradeValue;
        UpgradeCanvas.Instance.ShowCanvas(upgradeName, "Passive Attack Speed " + upgradeValue);
    }

    public override void UpgradeSpellProjectileSpeed(float upgradeValue, string upgradeName, BaseSpell spell)
    {
        spell.projectileSpeed += upgradeValue;
        UpgradeCanvas.Instance.ShowCanvas(upgradeName, "Passive Projectile Speed +" + upgradeValue);
    }

    public override void UpgradeSpellRange(float upgradeValue, string upgradeName, BaseSpell spell)
    {
        spell.range += upgradeValue;
        UpgradeCanvas.Instance.ShowCanvas(upgradeName, "Passive Range +" + upgradeValue);
    }

}
