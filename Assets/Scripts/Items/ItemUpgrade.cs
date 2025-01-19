using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemUpgrade", menuName = "Upgrades/ItemUpgrade", order = 1)]

public class ItemUpgrade : ScriptableObject
{
    public BaseSpell spell;

    public float upgradeValue;

    public string upgradeName;

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
                    spell.spellUpgrade.UpgradeSpellDamage(upgradeValue, upgradeName, spell);
                    break;

                case UpgradeType.AttackSpeed:
                    spell.spellUpgrade.UpgradeSpellAttackSpeed(upgradeValue, upgradeName, spell);
                    break;

                case UpgradeType.ProjectileSpeed:
                    spell.spellUpgrade.UpgradeSpellProjectileSpeed(upgradeValue, upgradeName, spell);
                    break;

                case UpgradeType.Range:
                    spell.spellUpgrade.UpgradeSpellRange(upgradeValue, upgradeName, spell);
                    break;
            }

            InventoryCanvas.Instance.AddItem(upgradeIcon);
            StatsCanvasManager.Instance.UpdateStats(Player.Instance);

        }
    }
}
