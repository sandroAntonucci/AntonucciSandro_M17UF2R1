using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo
{
    public string name;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour
{

    public static RoomController instance;

    string currentWorldName = "Basement";

    RoomInfo currentLoadRoomData;

    Room currRoom;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms { get; } = new List<Room>();

    bool isLoadingRoom = false;

    bool spawnedBossRoom = false;

    bool spawnedItemRoom = false;

    bool spawnedShopRoom = false;

    bool allRoomsSpawned = false;   

    bool updatedRooms = false;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        UpdateRoomQueue();
    }

    private void UpdateRoomQueue()
    {
        if (isLoadingRoom) return;

        if (loadRoomQueue.Count == 0)
        {
            
            if (!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if (!spawnedItemRoom)
            {
                StartCoroutine(SpawnItemRoom());
            }
            else if (!spawnedShopRoom)
            {
                StartCoroutine(SpawnShopRoom());
            }
            else if (allRoomsSpawned && !updatedRooms)
            {
                foreach (Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                updatedRooms = true;
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;

        yield return new WaitForSeconds(0.5f);

        if (loadRoomQueue.Count == 0)
        {
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Room bossTempRoom = new Room(bossRoom.X, bossRoom.Y);

            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == bossTempRoom.X && r.Y == bossTempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("Boss", bossTempRoom.X, bossTempRoom.Y);
        }

    }

    IEnumerator SpawnItemRoom()
    {

        spawnedItemRoom = true;

        yield return new WaitForSeconds(1f);

        if (loadRoomQueue.Count == 0)
        {

            int randRoomPosition = UnityEngine.Random.Range(1, loadedRooms.Count - 2);

            Room itemRoom = loadedRooms[randRoomPosition];
            Room itemTempRoom = new Room(itemRoom.X, itemRoom.Y);

            Destroy(itemRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == itemTempRoom.X && r.Y == itemTempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("Item", itemTempRoom.X, itemTempRoom.Y);
        }

    }

    // Last room to spawn
    IEnumerator SpawnShopRoom()
    {

        spawnedShopRoom = true;

        yield return new WaitForSeconds(1.5f);

        if (loadRoomQueue.Count == 0)
        {

            int randRoomPosition = randRoomPosition = UnityEngine.Random.Range(1, loadedRooms.Count - 3);

            Room shopRoom = loadedRooms[randRoomPosition];
            Room shopTempRoom = new Room(shopRoom.X, shopRoom.Y);


            Destroy(shopRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == shopTempRoom.X && r.Y == shopTempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("Shop", shopTempRoom.X, shopTempRoom.Y);
            
        }

        allRoomsSpawned = true;
    }

    public void LoadRoom(string name, int x, int y)
    {

        if (DoesRoomExist(x, y)) return;

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info) 
    {

        string roomName = currentWorldName + info.name;

        // This is used to load all the rooms in the same scene
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (!loadRoom.isDone)
        {
            yield return null;
        }

    }

    // Sets a room within the scene
    public void RegisterRoom(Room room)
    {

        if (!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3(
                currentLoadRoomData.X * room.Width,
                currentLoadRoomData.Y * room.Height,
                0
            );

            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if (loadedRooms.Count == 0)
            {
                CameraController.Instance.currRoom = room;
            }

            loadedRooms.Add(room);

        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }

    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.Instance.currRoom = room;
        currRoom = room;
    }

}
