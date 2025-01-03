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
        mage.SetActive(true);
        menuManager.PlayGame();
    }

    public void RogueChosen()
    {
        Destroy(mage);
        rogue.SetActive(true);
        menuManager.PlayGame();
    }


}
