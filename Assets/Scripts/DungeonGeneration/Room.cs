using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int Width;
    public int Height;
    public int X;
    public int Y;

    public bool playerInRoom = false;  

    public List<Enemy> enemyList;

    private bool updatedDoors = false;
    

    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    public List<Door> doors = new List<Door>();

    private void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.Log("You pressed play in the wrong scene!");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();

        foreach (Door d in ds) 
        {

            doors.Add(d);

            switch (d.doorType)
            {
                case Door.DoorType.right:
                    rightDoor = d; break;
                case Door.DoorType.left:
                    leftDoor = d; break;
                case Door.DoorType.top:
                    topDoor = d; break;
                case Door.DoorType.bottom:
                    bottomDoor = d; break;
            }

        }

        RoomController.instance.RegisterRoom(this);

    }

    private void Update()
    {
        if (name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }

        if (playerInRoom) CheckEnemies();
    }

    public void AddEnemies()
    {
        // Clear the list to avoid duplicates
        enemyList.Clear();

        // Find all components with the "Enemy" script in children, including inactive ones
        Enemy[] childEnemies = GetComponentsInChildren<Enemy>(true);

        // Add each enemy to the list
        foreach (Enemy enemy in childEnemies)
        {
            enemyList.Add(enemy);
            enemy.gameObject.SetActive(true);
        }

    }

    // Checks how many enemies are still alive in the room and opens or closes doors accordingly
    public void CheckEnemies()
    {

        bool allEnemiesDead = true;

        foreach (Enemy enemy in enemyList)
        {
            if (enemy != null)
            {
                allEnemiesDead = false;
            }
        }

        if (allEnemiesDead)
        {
            OpenDoors();
        }
        else
        {
            CloseDoors();
        }

    }

    // Opens doors when all enemies are defeated
    public void OpenDoors()
    {
        foreach (Door door in doors)
        {
            if(door.isActive)
            {
                door.GetComponent<BoxCollider2D>().enabled = false;
                door.GetComponent<Animator>().Play("Door_Open");
            }
        }
    }

    // Closes doors when there are still enemies in the room
    public void CloseDoors()
    {
        foreach (Door door in doors)
        {
            if (door.isActive)
            {
                door.GetComponent<BoxCollider2D>().enabled = true;
                door.GetComponent<Animator>().Play("Door_Closed");
            }
        }
    }

    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doorType) 
            {
                case Door.DoorType.right:
                    if(GetRight() == null)
                    {
                        door.GetComponent<Door>().RemoveDoor();
                    }
                    break;
                case Door.DoorType.left:
                    if (GetLeft() == null)
                    {
                        door.GetComponent<Door>().RemoveDoor();
                    }
                    break;
                case Door.DoorType.top:
                    if (GetTop() == null)
                    {
                        door.GetComponent<Door>().RemoveDoor();
                    }
                    break;
                case Door.DoorType.bottom:
                    if (GetBottom() == null)
                    {
                        door.GetComponent<Door>().RemoveDoor();
                    }
                    break;
            }

        }
    }

    public Room GetRight()
    {
        if(RoomController.instance.DoesRoomExist(X + 1, Y))
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }

        return null;
    }

    public Room GetLeft()
    {
        if (RoomController.instance.DoesRoomExist(X - 1, Y))
        {
            return RoomController.instance.FindRoom(X - 1, Y);
        }

        return null;

    }

    public Room GetTop()
    {
        if (RoomController.instance.DoesRoomExist(X, Y + 1))
        {
            return RoomController.instance.FindRoom(X, Y + 1);
        }

        return null;

    }

    public Room GetBottom()
    {
        if (RoomController.instance.DoesRoomExist(X, Y - 1))
        {
            return RoomController.instance.FindRoom(X, Y - 1);
        }

        return null;

    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * Width, Y * Height);
    }

    // Room transition
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
            AddEnemies();
            playerInRoom = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRoom = false;
        }
    }

}
