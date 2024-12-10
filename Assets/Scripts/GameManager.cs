using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

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

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {


        StartCoroutine(LoadSceneCanvas.Instance.ShowLoadingScreen(4f));


        DungeonCrawlerController.GenerateDungeon(DungeonGenerator.instance.dungeonGenerationData);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }


        yield return new WaitForSeconds(0.5f);

        StartCoroutine(Player.Instance.ReloadPlayer());

        StartCoroutine(RemoveScenes());

    }


    public IEnumerator RemoveScene(string sceneName) 
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);

        SceneManager.UnloadSceneAsync(scene);

        yield return null;

    }

    public IEnumerator RemoveScenes()
    {

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("BasementMain"));

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if (scene.name != "BasementMain")
            {
                SceneManager.UnloadSceneAsync(scene);
            }

        }

        yield return null;

    }

    public void ReloadCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        LoadScene(currentSceneName);
    }


}
