using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsObject : MonoBehaviour
{

    private Transform target;
    private Transform player;

    [SerializeField] private string targetTag;
    [SerializeField] private float rotationSpeed = 5f;

    void FixedUpdate()
    {

        if (player == null && GameObject.FindGameObjectWithTag("Player") != null) player = GameObject.FindGameObjectWithTag("Player").transform;
        if(target == null && GameObject.FindGameObjectWithTag(targetTag) != null) target = GameObject.FindGameObjectWithTag(targetTag).transform;

        if (target != null && player != null)
        {
            // Calculate direction to the target from the player's position
            Vector2 directionToTarget = (target.position - player.position).normalized;

            // Calculate the angle between the current forward direction and the target direction
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            // Smoothly rotate the object towards the target
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }

}
