using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemUpgrade : MonoBehaviour
{

    public BaseSpell spell;

    // Upgrades spell depending on the upgrade type
    public float upgradeValue;

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
                    spell.UpgradeSpellDamage(upgradeValue);
                    break;

                case UpgradeType.AttackSpeed:
                    spell.UpgradeSpellAttackSpeed(upgradeValue);
                    break;
                
                case UpgradeType.ProjectileSpeed:
                    spell.UpgradeSpellProjectileSpeed(upgradeValue);
                    break;
                
                case UpgradeType.Range:
                    spell.UpgradeSpellRange(upgradeValue);
                    break;

            }

            upgradeCanvas.ShowCanvas(upgradeName, upgradeText);

            Destroy(gameObject);

        }
    }
}
