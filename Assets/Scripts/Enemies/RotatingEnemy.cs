using UnityEngine;

public class RotatingEnemy : Enemy
{

    public float rotationSpeed = 5f;

    void FixedUpdate()
    {
        if (player != null)
        {

            Vector2 direction = player.position - transform.position;

            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            angle += 90;

            float smoothAngle = Mathf.LerpAngle(transform.eulerAngles.z, angle, rotationSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0, 0, smoothAngle);
        }
    }
}