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

    public void Start()
    {
        StartCoroutine(ShowLoadingScreen(3f));
    }

    public IEnumerator ShowLoadingScreen(float time)
    {
        canvas.enabled = true;
        StartCoroutine(textAnimation.TypeText(textAnimation.message));
        yield return new WaitForSeconds(time);
        canvas.enabled = false;

        levelCanvas.ShowCanvas();
    }

}
