using System.Collections;
using UnityEngine;
using TMPro;

public class LoadSceneCanvas : MonoBehaviour
{
    // Singleton Instance
    public static LoadSceneCanvas Instance { get; private set; }

    public Canvas canvas;
    public TextAnimation textAnimation;

    public LevelCanvas levelCanvas;

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

    public void ShowLoadingScreen()
    {
        canvas.enabled = true;
        StartCoroutine(textAnimation.TypeText(textAnimation.message));

    }

    public void HideLoadingScreen()
    {
        canvas.enabled = false;
        levelCanvas.ShowCanvas();
    }

}
