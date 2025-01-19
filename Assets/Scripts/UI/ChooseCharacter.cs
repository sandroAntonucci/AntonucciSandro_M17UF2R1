using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacter : MonoBehaviour
{

    [SerializeField] private GameObject mage;
    [SerializeField] private GameObject rogue;
    [SerializeField] private GameObject sorcerer;
    [SerializeField] private MenuManager menuManager;

    public void MageChosen()
    {
        Destroy(rogue);
        Destroy(sorcerer);
        mage.SetActive(true);
        StatsCanvasManager.Instance.SetMageStatsText();
        menuManager.PlayGame();
    }

    public void RogueChosen()
    {
        Destroy(mage);
        Destroy(sorcerer);
        rogue.SetActive(true);
        StatsCanvasManager.Instance.SetRogueStatsText();
        menuManager.PlayGame();
    }

    public void SorcererChosen()
    {
        Destroy(mage);
        Destroy(rogue);
        sorcerer.SetActive(true);
        StatsCanvasManager.Instance.SetSorcererStatsText();
        menuManager.PlayGame();
    }


}
