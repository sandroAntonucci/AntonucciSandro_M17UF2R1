using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemUpgrade : MonoBehaviour
{

    public BaseSpell spell;

    // Upgrades spell depending on the upgrade type
    public float damageUpgrade;
    public float attackSpeedUpgrade;
    public float projectileSpeedUpgrade;
    public float rangeUpgrade;

    // Upgrade Info
    public string upgradeName;
    public string upgradeText;
    public int upgradePrice;

    public UpgradeType upgradeType;

    public UpgradeCanvas upgradeCanvas;

    public enum UpgradeType
    {
        Damage,
        AttackSpeed,
        ProjectileSpeed,
        Range
    }

    public virtual void Start()
    {
        upgradeCanvas = GameObject.FindGameObjectWithTag("UpgradeCanvas").GetComponent<UpgradeCanvas>();
    }

    public virtual void Upgrade()
    {
        if (spell != null)
        {
            switch (upgradeType)
            {
               
                case UpgradeType.Damage:
                    UpgradeSpellDamage();
                    break;

                case UpgradeType.AttackSpeed:
                    UpgradeSpellAttackSpeed();
                    break;
                
                case UpgradeType.ProjectileSpeed:
                    UpgradeSpellProjectileSpeed();
                    break;
                
                case UpgradeType.Range:
                    UpgradeSpellRange();
                    break;

            }

            upgradeCanvas.ShowCanvas(upgradeName, upgradeText);

            Destroy(gameObject);

        }
    }


    // Upgrades the damage of the spell
    public void UpgradeSpellDamage()
    {
        spell.damage += damageUpgrade;
        spell.projectile.transform.localScale = new Vector3(
                                                            transform.localScale.x * 1.1f,
                                                            transform.localScale.y * 1.1f,
                                                            transform.localScale.z * 1.1f
                                                        );
    }

    // Upgrades the attack speed of the spell
    public void UpgradeSpellAttackSpeed()
    {
        if (spell.attackSpeed - attackSpeedUpgrade < 0.2) spell.attackSpeed = 0.1f;
        else
            spell.attackSpeed -= attackSpeedUpgrade;
    }

    // Upgrades the speed of the projectile
    public void UpgradeSpellProjectileSpeed()
    {
        spell.projectileSpeed += projectileSpeedUpgrade;
    }

    // Upgrades the range of the spell (only used in passive upgrade)
    public void UpgradeSpellRange()
    {
        spell.range += rangeUpgrade;
    }


}
