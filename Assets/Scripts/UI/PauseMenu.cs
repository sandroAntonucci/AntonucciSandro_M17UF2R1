using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MenuManager
{

    public static PauseMenu Instance { get; private set; }

    private InputAction pause;
    
    [SerializeField] private PlayerControls playerControls;

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

    public void OnEnable()
    {
        playerControls = new PlayerControls();

        pause = playerControls.Player.Pause;
        pause.Enable();
        pause.performed += context => GameManager.Instance.PauseOrUnpauseGame();
    }

    public void OnDisable()
    {
        pause.Disable();
    }

    public void ContinueGame()
    {
        GameManager.Instance.PauseOrUnpauseGame();
    }

    public override void ReturnToMenu()
    {
        ContinueGame();
        base.ReturnToMenu();
    }



}
