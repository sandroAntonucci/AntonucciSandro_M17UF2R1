using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsCanvasManager : MonoBehaviour
{

    public static StatsCanvasManager Instance { get; private set; }

    public TextMeshProUGUI spellDamageText;
    public TextMeshProUGUI spellAttackSpeedText;
    public TextMeshProUGUI spellProjectileSpeedText;

    public TextMeshProUGUI spellDamageValue;
    public TextMeshProUGUI spellAttackSpeedValue;
    public TextMeshProUGUI spellProjectileSpeedValue;

    public TextMeshProUGUI passiveDamageValue;
    public TextMeshProUGUI passiveAttackSpeedValue;
    public TextMeshProUGUI passiveProjectileSpeedValue;
    public TextMeshProUGUI passiveRangeValue;

    private float baseSpellDamage;
    private float baseSpellAttackSpeed;
    private float baseSpellProjectileSpeed;

    private float basePassiveDamage;
    private float basePassiveAttackSpeed;
    private float basePassiveProjectileSpeed;
    private float basePassiveRange;


    public Color baseColor;
    public Color maxColor;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }

    }

    public void SetMageStatsText()
    {
        spellDamageText.text = "Spell Damage";
        spellAttackSpeedText.text = "Spell A.S";
        spellProjectileSpeedText.text = "Spell P.S";
        SetBaseStats(Player.Instance);
        UpdateStats(Player.Instance);
    }

    public void SetRogueStatsText()
    {
        spellDamageText.text = "Knife Damage";
        spellAttackSpeedText.text = "Knife A.S";
        spellProjectileSpeedText.text = "Knife Size";
        SetBaseStats(Player.Instance);
        UpdateStats(Player.Instance);
    }

    public void SetSorcererStatsText()
    {
        spellDamageText.text = "Flame Damage";
        spellAttackSpeedText.text = "Flame Scale";
        spellProjectileSpeedText.text = "Flame P.S";
        SetBaseStats(Player.Instance);
        UpdateStats(Player.Instance);
    }

    public void UpdateStats(Player player)
    {
        spellDamageValue.text = player.spell.damage.ToString();
        spellDamageValue.color = GetGradientColor(player.spell.damage, baseSpellDamage);

        spellAttackSpeedValue.text = player.spell.attackSpeed.ToString();
        spellAttackSpeedValue.color = GetGradientColor(player.spell.attackSpeed, baseSpellAttackSpeed);

        spellProjectileSpeedValue.text = player.spell.projectileSpeed.ToString();
        spellProjectileSpeedValue.color = GetGradientColor(player.spell.projectileSpeed, baseSpellProjectileSpeed);

        passiveDamageValue.text = player.passiveSpell.damage.ToString();
        passiveDamageValue.color = GetGradientColor(player.passiveSpell.damage, basePassiveDamage);

        passiveAttackSpeedValue.text = player.passiveSpell.attackSpeed.ToString();
        passiveAttackSpeedValue.color = GetGradientColor(player.passiveSpell.attackSpeed, basePassiveAttackSpeed);

        passiveProjectileSpeedValue.text = player.passiveSpell.projectileSpeed.ToString();
        passiveProjectileSpeedValue.color = GetGradientColor(player.passiveSpell.projectileSpeed, basePassiveProjectileSpeed);

        passiveRangeValue.text = player.passiveSpell.range.ToString();
        passiveRangeValue.color = GetGradientColor(player.passiveSpell.range, basePassiveRange);
    }

    public void SetBaseStats(Player player)
    {
        baseSpellDamage = player.spell.damage;
        baseSpellAttackSpeed = player.spell.attackSpeed;
        baseSpellProjectileSpeed = player.spell.projectileSpeed;

        basePassiveDamage = player.passiveSpell.damage;
        basePassiveAttackSpeed = player.passiveSpell.attackSpeed;
        basePassiveProjectileSpeed = player.passiveSpell.projectileSpeed;
        basePassiveRange = player.passiveSpell.range;
    }

    private Color GetGradientColor(float value, float baseValue)
    {
        float maxValue = baseValue * 3;

        float normalizedValue = Mathf.Clamp01((value - baseValue) / (maxValue - baseValue));

        return Color.Lerp(baseColor, maxColor, normalizedValue);
    }

}
