using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeUpgrade : BaseUpgrade
{

    public override void UpgradeSpellDamage(float upgradeValue, string upgradeName, BaseSpell spell)
    {

        MeleeSpell melee = (MeleeSpell)spell;
        melee.damage += upgradeValue * 2f;
        melee.knife.damage += upgradeValue * 2f;
        UpgradeCanvas.Instance.ShowCanvas(upgradeName, "Knife Damage +" + upgradeValue);
    }

    public override void UpgradeSpellAttackSpeed(float upgradeValue, string upgradeName, BaseSpell spell)
    {
        spell.anim.speed += upgradeValue * -4;
        spell.attackSpeed += upgradeValue;
        UpgradeCanvas.Instance.ShowCanvas(upgradeName, "Knife Attack Speed " + upgradeValue);
    }

    // Upgrades the size of the knife
    public override void UpgradeSpellProjectileSpeed(float upgradeValue, string upgradeName, BaseSpell spell)
    {
        spell.transform.localScale += new Vector3(upgradeValue * 0.15f, upgradeValue * 0.15f, 0);
        spell.projectileSpeed += upgradeValue;
        UpgradeCanvas.Instance.ShowCanvas(upgradeName, "Knife Size +" + upgradeValue);
    }

}

