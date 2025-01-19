using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUpgrade : MonoBehaviour
{
    public abstract void UpgradeSpellDamage(float upgradeValue, string upgradeName, BaseSpell spell);

    public abstract void UpgradeSpellAttackSpeed(float upgradeValue, string upgradeName, BaseSpell spell);

    public abstract void UpgradeSpellProjectileSpeed(float upgradeValue, string upgradeName, BaseSpell spell);

    public virtual void UpgradeSpellRange(float upgradeValue, string upgradeName, BaseSpell spell) { }
}
