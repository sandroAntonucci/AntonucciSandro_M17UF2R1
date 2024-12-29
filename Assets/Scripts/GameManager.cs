using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
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

        LoadSceneCanvas.Instance.ShowLoadingScreen();

        // Disables player (not the game object)
        Player.Instance.gameObject.GetComponent<PlayerSpawn>().DisablePlayer();

        DungeonCrawlerController.GenerateDungeon(DungeonGenerator.instance.dungeonGenerationData);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; 

        while (asyncLoad.progress < 0.9f)
        {
            yield return null; 
        }

        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Minimum time to show the loading screen
        yield return new WaitForSeconds(2f);

        LoadSceneCanvas.Instance.HideLoadingScreen();

        // Reloads player
        Player.Instance.ReloadPlayer();

        yield return StartCoroutine(RemoveScenes());

        if(sceneName == "FinalBossMain")
        {
            Player.Instance.gameObject.transform.position = new Vector3(0, 7, 0);
            CameraController.Instance.gameObject.transform.position = new Vector3(0, 0, -10);
            CameraController.Instance.GetComponent<Camera>().orthographicSize = 9;
        }

    }



    public IEnumerator RemoveScene(string sceneName) 
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);

        SceneManager.UnloadSceneAsync(scene);

        yield return null;

    }

    public IEnumerator RemoveScenes()
    {

        string currentMain = RoomController.instance.currentWorldName + "Main";

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentMain));

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if (scene.name != currentMain)
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
