using System.Collections;
using TMPro;
using UnityEngine;

public class LevelCanvas : MonoBehaviour
{

    public static LevelCanvas Instance { get; private set; }

    public Animator nameBannerAnimator;
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
        nameBannerAnimator.Play("ShowUpgradeNameAnim");
    }

}
