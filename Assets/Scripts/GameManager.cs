using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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

    public void PauseOrUnpauseGame()
    {
        Canvas pauseCanvas = GameObject.FindGameObjectWithTag("PauseCanvas").GetComponent<Canvas>();
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (player == null) return;

        if (pauseCanvas.enabled)
        {
            pauseCanvas.enabled = false;
            Time.timeScale = 1; // Resume the game
            player.enabled = true; // Enable player input
        }
        else
        {
            pauseCanvas.enabled = true;
            Time.timeScale = 0; // Pause the game
            player.enabled = false; // Disable player input
        }
    }

    public void OpenOrCloseInventory()
    {

        Canvas inventory = GameObject.FindGameObjectWithTag("InventoryCanvas").GetComponent<Canvas>();
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (player == null) return;

        if (inventory.enabled)
        {
            inventory.enabled = false;
        }
        else
        {
            inventory.enabled = true;
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {

        LoadSceneCanvas.Instance.ShowLoadingScreen();

        // Disables player (not the game object)
        if(Player.Instance != null) Player.Instance.gameObject.GetComponent<PlayerSpawn>().DisablePlayer();

        if(DungeonGenerator.instance != null) DungeonCrawlerController.GenerateDungeon(DungeonGenerator.instance.dungeonGenerationData);

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

        // Disables Pausing and inventory
        GameObject pauseCanvas = GameObject.FindGameObjectWithTag("PauseCanvas");
        if (pauseCanvas != null) pauseCanvas.GetComponent<PauseMenu>().enabled = false;

        GameObject inventoryCanvas = GameObject.FindGameObjectWithTag("InventoryCanvas");
        if (inventoryCanvas != null) inventoryCanvas.GetComponent<InventoryCanvas>().enabled = false;

        // Minimum time to show the loading screen
        yield return new WaitForSeconds(3f);

        LoadSceneCanvas.Instance.HideLoadingScreen();

        if (sceneName == "MainScene")
        {
            if (Player.Instance != null) Destroy(Player.Instance.gameObject);
            if (pauseCanvas != null) pauseCanvas.GetComponent<PauseMenu>().enabled = false;
            Destroy(GameObject.FindGameObjectWithTag("MainCamera"));
        }


        // Reloads player
        if (Player.Instance != null) Player.Instance.ReloadPlayer();

        yield return StartCoroutine(RemoveScenes());

        if(sceneName == "FinalBossMain")
        {
            Player.Instance.gameObject.transform.position = new Vector3(0, 7, 0);
            CameraController.Instance.gameObject.transform.position = new Vector3(0, 0, -10);
            CameraController.Instance.GetComponent<Camera>().orthographicSize = 9;
        }

        // Enables pausing and inventory
        if (sceneName != "MainScene")
        {
            if (pauseCanvas != null) pauseCanvas.GetComponent<PauseMenu>().enabled = true;
            if (inventoryCanvas != null) inventoryCanvas.GetComponent<InventoryCanvas>().enabled = true;
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

        if (RoomController.instance != null)
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
        }

        yield return null;

    }

    public void ReloadCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        LoadScene(currentSceneName);
    }


}
