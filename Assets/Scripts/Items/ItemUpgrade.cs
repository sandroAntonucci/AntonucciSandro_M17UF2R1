using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemUpgrade : MonoBehaviour
{

    public string upgradeName;
    public string upgradeText;
    public UpgradeCanvas upgradeCanvas;

    public void Start()
    {
        upgradeCanvas = GameObject.FindGameObjectWithTag("UpgradeCanvas").GetComponent<UpgradeCanvas>();
    }

    public abstract void Upgrade();

}
