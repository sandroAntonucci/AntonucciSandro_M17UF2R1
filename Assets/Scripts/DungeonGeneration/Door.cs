using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    // This is used to make the door "inactive" without destroying it for collisions
    public bool isActive = true;

    public Sprite doorOpen;
    public Sprite doorClosed;

    public Sprite itemDoorOpen;
    public Sprite itemDoorClosed;

    public Sprite shopDoorOpen;
    public Sprite shopDoorClosed;

    public Sprite bossDoorOpen;
    public Sprite bossDoorClosed;

    public SpriteRenderer spriteRenderer;

    public enum DoorType
    {
        left, right, top, bottom
    }

    public DoorType doorType;


    // Removes vision of the door so it's still collidable but not visible
    public void RemoveDoor()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
        isActive = false;
    }

    public void ChangeSprite(string nextRoomName, bool isOpen)
    {
        if (nextRoomName.Contains("Room") || nextRoomName.Contains("Start"))
        {
            spriteRenderer.sprite = isOpen ? doorOpen : doorClosed;
        }

        else if (nextRoomName.Contains("Item"))
        {
            spriteRenderer.sprite = isOpen ? itemDoorOpen : itemDoorClosed;
        }

        else if (nextRoomName.Contains("Shop"))
        {
            spriteRenderer.sprite = isOpen ? shopDoorOpen : shopDoorClosed;
        }

        else if (nextRoomName.Contains("Boss"))
        {
            spriteRenderer.sprite = isOpen ? bossDoorOpen : bossDoorClosed;
        }
    }
    

}
