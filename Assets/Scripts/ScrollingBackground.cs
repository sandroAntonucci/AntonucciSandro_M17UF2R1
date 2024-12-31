using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private Transform player;

    [SerializeField] private float parallaxFactor = 0.5f; 

    private Vector2 offset;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        offset.x = player.position.x * parallaxFactor;
        offset.y = player.position.y * parallaxFactor;

        transform.position = offset;
    }
}