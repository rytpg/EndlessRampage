using System;
using UnityEngine;

public class DDAController : MonoBehaviour
{

    [Range(0.5f,5f)] public float baseDifficulty = 1.0f;
    public float difficultyMultiplier = 1.0f;
    [Range(0f,1f)] public float healthPickupDropChance = 0.5f;
    [Range(0f,1f)] public float minHealthPickupDropChance = 0.15f;
    [Range(0f,1f)] public float maxHealthPickupDropChance = 0.75f;

    public HealthManager playerHealth;

    //Tracking wave stats
    private float waveDamageTaken;
    private float waveStartTime;
    private float prevHealth;

    private bool waveRunning;

    void Start()
    {
        if (playerHealth == null)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<HealthManager>();
        }
        
        if(playerHealth != null)
        {
            prevHealth = playerHealth.GetHealth();
        }

        UpdateHealthPickupDropChance();
    }

    void Update()
    {
        if(!waveRunning || playerHealth == null) return;

        float currentHealth = playerHealth.GetHealth();
        if(currentHealth < prevHealth)
        {
            waveDamageTaken += (prevHealth - currentHealth);
        }
        prevHealth = currentHealth;
        UpdateHealthPickupDropChance();
    }

    public void OnStartWave(int waveNumber)
    {
        //update variables
        waveRunning = true;
        waveDamageTaken = 0f;
        waveStartTime = Time.time;

        if(playerHealth != null)
        {
            prevHealth = playerHealth.GetHealth();
        }

        UpdateHealthPickupDropChance();
    }

    public void OnWaveEnd(int waveNumber)
    {
        waveRunning = false;

        if(playerHealth == null) return;

        float healthPercentage = playerHealth.GetHealth() / playerHealth.maxHealth;
        
        //maps waveDamageTaken into 0-1 scale (5 damage taken = 0 damageScore etc)
        float damageScore = Mathf.InverseLerp(6f, 0f, waveDamageTaken);
        
        //weighting
        float score = (damageScore * 0.6f) + (healthPercentage * 0.4f);
        //Converting performance score to difficulty multiplier
        // eg score 0 - multiplier 0.8, score 1 - multiplier = 1.2
        // 20% +- difficulty swing
        float target = Mathf.Lerp(0.6f, 1.6f, score);

        float oldDifficulty = difficultyMultiplier;

        //smoothing, so it doesnt jump
        difficultyMultiplier = Mathf.Lerp(difficultyMultiplier, target, 0.35f);

        //cant go below 0.6 difficulty or above 1.5, make these fields later
        difficultyMultiplier = Mathf.Clamp(difficultyMultiplier, 0.6f, 1.5f);

        StatTracker.instance?.LogDDAEvent(
            Time.time,
            waveNumber,
            waveDamageTaken,
            healthPercentage,
            score,
            oldDifficulty,
            difficultyMultiplier);
        Debug.Log($"Wave {waveNumber} end, Damage = {waveDamageTaken}, HP = {healthPercentage}, multiplier = {difficultyMultiplier}");
    
    }

    void UpdateHealthPickupDropChance()
    {
        if (playerHealth == null || playerHealth.maxHealth <= 0f)
        {
            return;
        }

        float healthPercentage = playerHealth.GetHealth() / playerHealth.maxHealth;

        //1 = struggling hard on health, 0 when health
        float struggle = 1f - healthPercentage;

        healthPickupDropChance = Mathf.Lerp(minHealthPickupDropChance,maxHealthPickupDropChance,struggle);
    }

}
