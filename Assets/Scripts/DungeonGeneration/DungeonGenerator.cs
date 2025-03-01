using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DungeonGenerator : MonoBehaviour
{

    public static DungeonGenerator instance;

    public RoomController roomController;

    public DungeonGenerationData dungeonGenerationData;

    private int numberOfRooms;

    private List<Vector2Int> dungeonRooms;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ReloadRooms();
    }

    public void ReloadRooms()
    {

        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);

        SpawnRooms(dungeonRooms);

        foreach (Room room in RoomController.instance.loadedRooms)
        {
            room.RemoveUnconnectedDoors();
        }
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        roomController.LoadRoom("Start", 0, 0);

        // If the current world is the final boss room, do not load any other rooms
        if (RoomController.instance.currentWorldName == "FinalBoss")
        {
            return;
        }

        int numberOfRooms = 0;

        #if UNITY_EDITOR
                EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

                foreach (EditorBuildSettingsScene scene in scenes)
                {
                    if (scene.path.Contains(roomController.currentWorldName + "Room"))
                    {
                        numberOfRooms++;
                    }
                }
        #endif

        foreach (Vector2Int roomLocation in rooms)
        {
            // Loads a random room starting from 0 to the number of rooms in the scene build settings
            string sceneName = "Room" + Random.Range(0, numberOfRooms);

            roomController.LoadRoom(sceneName, roomLocation.x, roomLocation.y);
        }
    }



}
