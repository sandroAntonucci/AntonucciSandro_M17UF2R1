using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{

    [SerializeField] private bool spawnedByBoss = false;

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

        // If the spawner is spawned by a boss, it will not spawn waves by itself
        if (spawnedByBoss) return;

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

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnWave();
            yield return new WaitForSeconds(waveSpawnTime);
        }
    }

    // Spawns a random wave
    public void SpawnWave()
    {
        int randomWave = Random.Range(0, waves.Count);
        StartWave(waves[randomWave]);

    }
}
