using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnekromancerBoss : Boss
{

    [SerializeField] private CasterComponent casterComponent;
    [SerializeField] private EnemyWaveSpawner waveSpawner;
    [SerializeField] private Animator anim;

    public override void Start()
    {
        base.Start();
        gameObject.SetActive(true);
    }

    private void Spawn()
    {
        casterComponent.enabled = true;
        waveSpawner.enabled = true;
    }


}
