using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    // This is used to make the door "inactive" without destroying it for collisions
    public bool isActive = true;
    
    public enum DoorType
    {
        left, right, top, bottom
    }

    public DoorType doorType;

}
