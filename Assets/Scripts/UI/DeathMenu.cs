using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MenuManager
{

    public DeathMenu Instance;

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

    public override void ReturnToMenu()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
        base.ReturnToMenu();
    }

}
