using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCanvas : MonoBehaviour
{

    public static LevelCanvas Instance { get; private set; }

    public Animator nameBannerAnimator;

    public Image bannerImage;
    
    public Sprite catacombsBanner;
    public Sprite basementBanner;
    public Sprite sanctuaryBanner;

    public TextMeshProUGUI levelName;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

    }

    public void ShowCanvas()
    {
        levelName.text = RoomController.instance.currentWorldName;

        switch (levelName.text)
        {
            case "Catacombs":
                bannerImage.sprite = catacombsBanner;
                break;

            case "Basement":
                bannerImage.sprite = basementBanner;
                break;

            case "Sanctuary":
                bannerImage.sprite = sanctuaryBanner;
                break;
        }

        nameBannerAnimator.Play("ShowUpgradeNameAnim");
    }

}
