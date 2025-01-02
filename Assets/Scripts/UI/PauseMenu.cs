using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MenuManager
{

    private PauseMenu Instance;
    private InputAction pause;
    [SerializeField] private PlayerControls playerControls;

    private void Awake()
    {
        if (Instance != null )
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
