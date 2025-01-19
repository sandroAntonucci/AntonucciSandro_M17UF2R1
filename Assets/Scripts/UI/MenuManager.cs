using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public void ShowCharacterPick()
    {
        GameObject.FindGameObjectWithTag("ChooseCharacterCanvas").GetComponent<Canvas>().enabled = true;    
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void PlayGame()
    {
        GameManager.Instance.LoadScene("CatacombsMain");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public virtual void ReturnToMenu()
    {
        Time.timeScale = 1;
        GameManager.Instance.LoadScene("MainScene");

    }


}
