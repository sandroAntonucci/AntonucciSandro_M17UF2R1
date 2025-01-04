using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemUpgrade", menuName = "Upgrades/ItemUpgrade", order = 1)]

public class ItemUpgrade : ScriptableObject
{
    public BaseSpell spell;

    public float upgradeValue;

    public int upgradePrice;

    public Sprite upgradeIcon;

    public UpgradeType upgradeType;

    public bool isPassiveUpgrade;

    public enum UpgradeType
    {
        Damage,
        AttackSpeed,
        ProjectileSpeed,
        Range
    }

    public void ApplyUpgrade()
    {

        if (isPassiveUpgrade)
        {
            spell = Player.Instance.passiveSpell;
        }
        else
        {
            spell = Player.Instance.spell;
        }


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
        }
    }
}
