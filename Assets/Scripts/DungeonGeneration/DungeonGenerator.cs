using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonGenerator : MonoBehaviour
{

    public DungeonGenerationData dungeonGenerationData;

    private int numberOfRooms;

    private List<Vector2Int> dungeonRooms;

    private void Start()
    {

        // Get the number of rooms in the scene build settings for random room generation (substracts 5 for main, start, item shop, free item and end)
        numberOfRooms = SceneManager.sceneCountInBuildSettings - 5;

        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
        foreach(Room room in RoomController.instance.loadedRooms)
        {
            room.RemoveUnconnectedDoors();
        }
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomController.instance.LoadRoom("Start", 0, 0);

        foreach(Vector2Int roomLocation in rooms)
        {

            // Loads a random room starting from 0 to the number of rooms in the scene build settings
            string sceneName = "Room" + Random.Range(0, numberOfRooms);

            RoomController.instance.LoadRoom(sceneName, roomLocation.x, roomLocation.y);

        }
    }


}
