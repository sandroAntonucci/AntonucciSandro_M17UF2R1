using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

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
        GameManager.Instance.LoadScene("MainScene");
    }


}
