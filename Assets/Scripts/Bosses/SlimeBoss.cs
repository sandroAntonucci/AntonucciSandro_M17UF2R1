using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : Boss
{

    private DashComponent dashComponent;

    [SerializeField] private CasterComponent casterComponent;
    [SerializeField] private EnemyWaveSpawner waveSpawner;
    [SerializeField] private Animator anim;

    private bool isSpawning = true;

    private void OnEnable()
    {
        dashComponent = GetComponent<DashComponent>();
        casterComponent.enabled = true;
        waveSpawner.enabled = true;
        StartCoroutine(Idle());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        casterComponent.enabled = false;
        waveSpawner.enabled = false;
    }

    private IEnumerator Idle()
    {
        dashComponent.canDash = false;

        if(anim != null) anim.Play("Idle");

        if (isSpawning)
        {
            yield return new WaitForSeconds(1f);
            isSpawning = false;
        }

        // Resets caster attack patterns
        casterComponent.isActive = true;
        StartCoroutine(casterComponent.AttackLoop());

        yield return new WaitForSeconds(2f);

        StartCoroutine(DashAttack());
    }

    private IEnumerator DashAttack()
    {

        // Stops caster attack patterns
        casterComponent.isActive = false;
        casterComponent.StopAllCasters();

        anim.Play("Move");
        dashComponent.canDash = true;

        yield return new WaitForSeconds(4.5f);
        StartCoroutine(Idle());
    }


}
