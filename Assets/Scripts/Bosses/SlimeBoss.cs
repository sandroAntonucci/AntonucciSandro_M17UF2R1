using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : Boss
{

    private DashComponent dashComponent;

    [SerializeField] private EnemyWaveSpawner waveSpawner;
    [SerializeField] private Animator anim;
    
    public override void Start()
    {
        base.Start();
        dashComponent = GetComponent<DashComponent>(); 
    }

    private void OnEnable()
    {
        waveSpawner.enabled = true;
        StartCoroutine(Idle());
    }

    private void OnDisable()
    {
        waveSpawner.enabled = false;
        StopAllCoroutines();
    }

    private IEnumerator Idle()
    {
        dashComponent.canDash = false;
        anim.Play("Idle");

        yield return new WaitForSeconds(2f);

        StartCoroutine(DashAttack());
    }

    private IEnumerator DashAttack()
    {
        anim.Play("Move");
        dashComponent.canDash = true;

        yield return new WaitForSeconds(4.5f);
        StartCoroutine(Idle());
    }


}
