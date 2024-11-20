using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class UpgradeCanvas : MonoBehaviour
{

    public TextMeshProUGUI upgradeText;
    public TextMeshProUGUI upgradeName;
    public Canvas upgradeCanvas;

    public void ShowCanvas(string upgradeName, string upgradeText)
    {
        SetUpgradeName(upgradeName);
        SetUpgradeText(upgradeText);
        StartCoroutine(ShowCanvasCorroutine());
    }

    public void SetUpgradeText(string text)
    {
        upgradeText.text = text;
    }

    public void SetUpgradeName(string text) 
    {
        upgradeName.text = text;
    }

    public IEnumerator ShowCanvasCorroutine()
    {
        upgradeCanvas.enabled = true;
        yield return new WaitForSeconds(2);
        upgradeCanvas.enabled = false;
    }


}
