using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class KnekromancerBoss : Boss
{
    [SerializeField] private CasterComponent casterComponent;
    [SerializeField] private EnemyWaveSpawner waveSpawner;

    public UnityEvent OnBossDied;

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

    public override void Die()
    {
        OnBossDied?.Invoke();
        Debug.Log("This is executed");
        base.Die();
    }
}

