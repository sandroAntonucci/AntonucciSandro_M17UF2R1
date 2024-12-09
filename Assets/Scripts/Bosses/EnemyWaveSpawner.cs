using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{

    [SerializeField] private float waveSpawnTime = 8f;

    public GameObject[] waveOne;
    public GameObject[] waveTwo;
    public GameObject[] waveThree;

    private List<GameObject[]> waves;

    private void Start()
    {
        waves = new List<GameObject[]>
        {
            waveOne,
            waveTwo,
            waveThree
        };

        StartCoroutine(SpawnLoop());
    }

    private void StartWave(GameObject[] wave)
    {
        if (wave == null) return;
        foreach (var enemy in wave)
            if (enemy != null)
            {
                GameObject enemyCopy = Instantiate(enemy, enemy.transform.position, Quaternion.identity);
                enemyCopy.SetActive(true);
            }
    }

    // Spawns random waves
    private IEnumerator SpawnLoop()
    {

        while (true)
        {
            int randomWave = Random.Range(0, waves.Count);
            StartWave(waves[randomWave]);

            yield return new WaitForSeconds(waveSpawnTime);
        }

    }
}
