using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsCanvasManager : MonoBehaviour
{

    public TextMeshProUGUI spellDamageText;
    public TextMeshProUGUI spellAttackSpeedText;
    public TextMeshProUGUI spellProjectileSpeedText;

    public TextMeshProUGUI passiveDamageText;
    public TextMeshProUGUI passiveAttackSpeedText;
    public TextMeshProUGUI passiveProjectileSpeedText;
    public TextMeshProUGUI passiveRangeText;

    public void SetMageStatsText()
    {
        spellDamageText.text = "Spell Damage";
        spellAttackSpeedText.text = "Spell A.S";
        spellProjectileSpeedText.text = "Spell P.S";
    }

    public void SetRogueStatsText()
    {
        spellDamageText.text = "Knife Damage";
        spellAttackSpeedText.text = "Knife A.S";
        spellProjectileSpeedText.text = "Knife Size";
    }

    public void SetSorcererStatsText()
    {
        spellDamageText.text = "Flame Damage";
        spellAttackSpeedText.text = "Flame Scale";
        spellProjectileSpeedText.text = "Flame P.S";
    }

}
