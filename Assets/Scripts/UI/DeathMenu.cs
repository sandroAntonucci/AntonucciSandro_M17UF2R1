using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MenuManager
{

    public static DeathMenu Instance { get; private set; }

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
