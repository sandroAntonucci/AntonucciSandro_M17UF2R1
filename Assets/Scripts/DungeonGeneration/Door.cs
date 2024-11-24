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

    public SpriteRenderer spriteRenderer;

    public enum DoorType
    {
        left, right, top, bottom
    }

    public DoorType doorType;

    public string connectedRoom;

    public void Start()
    {
        connectedRoom = "Normal";
    }

    // Removes vision of the door so it's still collidable but not visible
    public void RemoveDoor()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
        isActive = false;
    }

    public void ChangeSprite(string doorSprite)
    {
        if (doorSprite == "doorOpen") spriteRenderer.sprite = doorOpen;
        if (doorSprite == "doorClosed") spriteRenderer.sprite = doorClosed;
        if (doorSprite == "itemDoorOpen") spriteRenderer.sprite = itemDoorOpen;
        if (doorSprite == "itemDoorClosed") spriteRenderer.sprite = itemDoorClosed;
        if (doorSprite == "shopDoorOpen") spriteRenderer.sprite = shopDoorOpen;
        if (doorSprite == "shopDoorClosed") spriteRenderer.sprite = shopDoorClosed;
    }

    

}
