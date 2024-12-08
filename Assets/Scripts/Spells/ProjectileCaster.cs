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

    public bool isShooting = true;

    public Stack<GameObject> projectilePool = new Stack<GameObject>();

    private void Start()
    {
        // Start the shooting coroutine
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


        BaseProjectile baseProjectile = newProjectile.GetComponent<BaseProjectile>();

        if (baseProjectile != null)
        {
            baseProjectile.caster = this;
            baseProjectile.projectileSpeed = projectileSpeed;
            baseProjectile.projectileDamage = damage;

            Vector2 shootDirection = shootPosition.right;
            baseProjectile.Shoot(shootDirection);
        }
    }

}
