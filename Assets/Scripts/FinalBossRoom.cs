using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering.Universal; 

public class FinalBossRoom : MonoBehaviour
{

    [SerializeField] private KnekromancerBoss boss;

    [SerializeField] private SpriteRenderer background;
    [SerializeField] private Sprite skyBackground;

    [SerializeField] private Tilemap ground;
    [SerializeField] private Tilemap groundDetails;

    [SerializeField] private GameObject bossDeathEffect;

    [SerializeField] private Light2D globalLight;

    private void OnEnable()
    {
        boss.OnBossDied.AddListener(ChangeWorld); 
    }

    private void ChangeWorld()
    {
        bossDeathEffect.SetActive(true);

        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }

        foreach (GameObject projectile in GameObject.FindGameObjectsWithTag("EnemySpell"))
        {
            Destroy(projectile);
        }


        ground.color = new Color(1f, 1f, 1f, 1);
        groundDetails.color = new Color(1f, 1f, 1f, 1);

        globalLight.intensity = 1.0f;
        globalLight.color = Color.white;

        background.sprite = skyBackground;
    }

}
