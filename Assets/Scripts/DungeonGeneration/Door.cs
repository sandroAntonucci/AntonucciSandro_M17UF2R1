using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    // This is used to make the door "inactive" without destroying it for collisions
    public bool isActive = true;

    // Removes vision of the door so it's still collidable but not visible
    public void RemoveDoor()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
        isActive = false;
    }

    public enum DoorType
    {
        left, right, top, bottom
    }

    public DoorType doorType;

}
