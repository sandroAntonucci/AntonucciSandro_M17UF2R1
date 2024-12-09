using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCaster : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPosition;

    // Caster properties
    [SerializeField] private float shootRate = 0.5f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private bool shootsTowardsPlayer = false;

    public bool isShooting;

    public Stack<GameObject> projectilePool = new Stack<GameObject>();

    private void OnEnable()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true) // Keeps trying to shoot as long as the script is active
        {
            if (isShooting)
            {
                CastProjectile();
            }
            yield return new WaitForSeconds(shootRate);
        }
    }

    private void CastProjectile()
    {
        GameObject newProjectile;

        if (projectilePool.Count > 0)
        {
            newProjectile = projectilePool.Pop();
            newProjectile.transform.position = shootPosition.position;
            newProjectile.SetActive(true);
        }
        else
        {
            newProjectile = Instantiate(projectilePrefab, shootPosition.position, Quaternion.identity);
        }


        EnemyProjectile baseProjectile = newProjectile.GetComponent<EnemyProjectile>();

        if (baseProjectile != null)
        {
            baseProjectile.caster = this;
            baseProjectile.projectileSpeed = projectileSpeed;
            baseProjectile.projectileDamage = damage;

            Vector2 shootDirection = shootPosition.right;

            // Shoots towards the player if the property is set to true
            if (shootsTowardsPlayer)
            {
                baseProjectile.CastTowardsPlayer();
                return;
            }

            // Shoots in the given direction
            baseProjectile.Shoot(shootDirection);
        }
    }

    public void DestroyProjectiles()
    {
        foreach (GameObject projectile in projectilePool)
        {
            Destroy(projectile);
        }
    }
}
