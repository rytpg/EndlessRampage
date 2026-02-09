using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class WaveSpawner : MonoBehaviour
{

    public int currentWave = 1;
    public float waveDuration = 10f;
    public int pointsPerWave = 10;


    public List<Enemy> enemies = new List<Enemy>();
    private List<GameObject> enemiesToSpawn = new List<GameObject>();
    private List<GameObject> SpawnedEnemies = new List<GameObject>();

    public Transform[] spawnLocations;

    private float waveTimer;
    private float spawnTimer;
    private float spawnInterval;
    private int spawnIndex;

    private bool waveActive;

    void Start()
    {
        StartWave(currentWave);
    }

    void Update()
    {
        if (!waveActive)
        {
            return;
        }

        SpawnedEnemies.RemoveAll(e => e == null);

        waveTimer -= Time.deltaTime;
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f && enemiesToSpawn.Count > 0)
        {
            SpawnNext();
            spawnTimer = spawnInterval;
        }

        if (waveTimer <= 0f && enemiesToSpawn.Count == 0 && SpawnedEnemies.Count == 0)
        {
            currentWave++;
            StartWave(currentWave);
        }
    }



    void StartWave(int waveNumber)
    {
        waveActive = true;
        waveTimer = waveDuration;
        spawnTimer = 0f;
        spawnIndex = 0;

        enemiesToSpawn.Clear();
        GenerateEnemiesForWave(waveNumber, enemiesToSpawn);

        if(enemiesToSpawn.Count == 0)
        {
            Debug.LogWarning("wave had 0 enemies. issue someewhere");
            waveActive = false;
            return;
        }
        spawnInterval = waveDuration / enemiesToSpawn.Count;

    }

    void SpawnNext()
    {
        if (spawnLocations == null || spawnLocations.Length == 0)
        {
            Debug.LogWarning("No spawn locations found");
            waveActive = false;
            return;
        }

        Transform spawn = spawnLocations[spawnIndex];
        spawnIndex = (spawnIndex + 1) % spawnLocations.Length;

        GameObject prefab = enemiesToSpawn[0];
        enemiesToSpawn.RemoveAt(0);

        GameObject enemy = Instantiate(prefab, spawn.position, Quaternion.identity);
        SpawnedEnemies.Add(enemy);

    }

    void GenerateEnemiesForWave(int waveNumber, List<GameObject> output)
    {
        int waveValue = waveNumber * pointsPerWave;
        
        int maxEnemies = 50;
        int tries = 0;
        int maxTries = 300;

        while(waveValue > 0 && output.Count < maxEnemies && tries < maxTries)
        {
            tries ++;

            var e = enemies[Random.Range(0, enemies.Count)];
            if (e.cost <= 0 || e.enemyPrefab == null)
            {
                continue;
            }

            if(waveValue - e.cost >= 0)
            {
                output.Add(e.enemyPrefab);
                waveValue -= e.cost;
            }
            else
            {
                
            }
        }
        Debug.Log($"Wave {waveNumber}, Spawning {output.Count} enemies");
    }
}





[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost = 1;
}
