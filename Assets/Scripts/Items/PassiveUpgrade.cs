using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveUpgrade : ItemUpgrade
{

    public PassiveSpell passiveSpell;

    public UpgradeType upgradeType;

    public enum UpgradeType
    {
        Damage,
        AttackSpeed,
        ProjectileSpeed,
        Range
    }

    public float damageUpgrade;
    public float attackSpeedUpgrade;
    public float projectileSpeedUpgrade;
    public float rangeUpgrade;


    // Upgrades passive spell depending on the upgrade type
    public override void Upgrade()
    {

        // Gets the passive spell from the player
        passiveSpell = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().passiveSpell;

        if (passiveSpell != null) 
        {
            switch (upgradeType)
            {
                case UpgradeType.Damage:
                    passiveSpell.damage += damageUpgrade;
                    
                    break;
                case UpgradeType.AttackSpeed:
                    passiveSpell.attackSpeed += attackSpeedUpgrade;
                    break;
                case UpgradeType.ProjectileSpeed:
                    passiveSpell.projectileSpeed += projectileSpeedUpgrade;
                    break;
                case UpgradeType.Range:
                    passiveSpell.range += rangeUpgrade;
                    break;
            }

            upgradeCanvas.ShowCanvas(upgradeName, upgradeText);

        }
    }


}
