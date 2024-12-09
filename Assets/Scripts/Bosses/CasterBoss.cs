using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class CasterBoss : MonoBehaviour
{
    [SerializeField] private float attackChangeTime = 5f;

    // Different attack patterns
    public ProjectileCaster[] attackCasterOne;
    public ProjectileCaster[] attackCasterTwo;
    public ProjectileCaster[] attackCasterThree;

    // List of all attack patterns
    private List<ProjectileCaster[]> attackCasters;

    private void Start()
    {
        // Stores all attack patterns to the list (this is done because you can't edit a list directly from the inspector, only arrays)
        attackCasters = new List<ProjectileCaster[]>
        {
            attackCasterOne,
            attackCasterTwo,
            attackCasterThree
        };

        StartCoroutine(AttackLoop());
    }

    private void StartAttack(ProjectileCaster[] casters)
    {
        if (casters == null) return;
        foreach (var caster in casters)
            if (caster != null)
            {

                caster.isShooting = true;
            }
    }

    private void StopAttack(ProjectileCaster[] casters)
    {
        if (casters == null) return;
        foreach (var caster in casters)
        {
            if (caster != null) caster.isShooting = false;
        }
    }

    private void StopAllCasters()
    {
        foreach (var casterGroup in attackCasters)
            StopAttack(casterGroup);
    }

    private IEnumerator AttackLoop()
    {
        while (true)
        {
            // Stop all attacks before starting a new one
            StopAllCasters();

            // Randomly choose an attack index
            int attackIndex = Random.Range(0, attackCasters.Count);

            // Start the chosen attack
            StartAttack(attackCasters[attackIndex]);

            yield return new WaitForSeconds(attackChangeTime);
        }
    }

}
