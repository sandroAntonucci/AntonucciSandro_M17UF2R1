using System.Collections;
using TMPro;
using UnityEngine;

public class UpgradeCanvas : MonoBehaviour
{

    public static UpgradeCanvas Instance { get; private set; }

    [SerializeField] private AudioSource upgradeSFX;

    public Animator nameBannerAnimator;
    public Animator textBannerAnimator;
    public RectTransform nameBannerRect;
    public RectTransform textBannerRect;
    public TextMeshProUGUI upgradeText;
    public TextMeshProUGUI upgradeName;
    public Canvas upgradeCanvas;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ShowCanvas(string upgradeName, string upgradeText)
    {
        SetUpgradeName(upgradeName);
        SetUpgradeText(upgradeText);

        upgradeSFX.Play();

        nameBannerAnimator.Play("ShowUpgradeNameAnim");
        textBannerAnimator.Play("ShowUpgradeAnim");

    }

    public void SetUpgradeText(string text)
    {
        upgradeText.text = text;
    }

    public void SetUpgradeName(string text)
    {
        upgradeName.text = text;
    }

}
