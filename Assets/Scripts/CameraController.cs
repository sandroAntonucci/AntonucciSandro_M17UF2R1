using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    public Room currRoom;
    public GameObject player;

    public float moveSpeedWhenRoomChange;

    private bool hasPlayerMoved; // Tracks if the player position has been updated for this transition

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist across scenes
    }

    void Update()
    {
        UpdatePosition();
    }

    void UpdatePlayerPosition(Vector3 targetPos)
    {
        if (player == null || hasPlayerMoved) // Prevent updating multiple times
        {
            return;
        }

        Vector3 direction = targetPos - transform.position;
        Vector3 newPosition = player.transform.position;

        // Adjust player's position based on the direction of the camera movement
        if (direction.x > 0)
        {
            newPosition.x += 4f;
        }
        else if (direction.x < 0)
        {
            newPosition.x -= 4f;
        }
        else if (direction.y > 0)
        {
            newPosition.y += 4f;
        }
        else if (direction.y < 0)
        {
            newPosition.y -= 4f;
        }

        player.transform.position = newPosition;

        hasPlayerMoved = true; // Mark that the player's position has been updated
    }

    void UpdatePosition()
    {
        if (currRoom == null)
        {
            return;
        }

        Vector3 targetPos = GetCameraTargetPosition();

        // Update player's position only once per transition
        if (!hasPlayerMoved)
        {
            UpdatePlayerPosition(targetPos);
        }

        // Move the camera towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeedWhenRoomChange);

        // Reset the flag when the camera reaches the target position
        if (transform.position == targetPos)
        {
            hasPlayerMoved = false;
        }
    }

    Vector3 GetCameraTargetPosition()
    {
        if (currRoom == null)
        {
            return Vector3.zero;
        }

        Vector3 targetPos = currRoom.GetRoomCentre();
        targetPos.z = transform.position.z;

        return targetPos;
    }

    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}
