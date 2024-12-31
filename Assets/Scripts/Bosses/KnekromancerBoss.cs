using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class KnekromancerBoss : Boss
{
    [SerializeField] private CasterComponent casterComponent;
    [SerializeField] private EnemyWaveSpawner waveSpawner;
    [SerializeField] private Animator anim;

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
        StartCoroutine(DieRoutine());
    }

    private IEnumerator DieRoutine()
    {
        anim.Play("Die");

        // Wait for the death animation to finish
        yield return new WaitForSeconds(0.8f);

        OnBossDied?.Invoke();

        base.Die();
    }
}

