using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


//This will take in everything I need for logging/ddaa
public class StatTracker : MonoBehaviour
{

    /*
    Creating a instance reference to self.
    by pointing to self i can reach it without having to create
    a reference in each script that i need it for.
    so instead of statTracker = findobjectoftype<stattracker>
    statTracker?.Log 
    i can just use StatTracker.instance.whatevermethod()


    */
    public static StatTracker instance;

    public class WaveLog
    {
        public int waveNumber;
        public int enemiesKilledThisWave;
    }

    public class TimeSeriesLog
    {
        public float timeStamp;
        public int waveNumber;
        public float playerHealth;
        public float playerHealthPercentage;
        public int enemiesAlive;
        public int enemiesKilledThisWave;
        public float difficultyMultiplier;
        public float healthPickupDropChance;
    }


    public class DDAEvent
    {
        public float timeStamp;
        public int waveNumber;
        public float damageTaken;
        public float healthPercentage;
        public float performanceScore;
        public float oldDifficulty;
        public float newDifficulty;
    }

    public class DamageEvent
    {
        public float timeStamp;
        public float damageAmount;
        public float playerHealthBefore;
        public float playerHealthAfter;
    }

    public class HealEvent
    {
        public float timeStamp;
        public float healAmount;
        public float healthBefore;
        public float healthAfter;
    }

    public List<WaveLog> waveLogs = new List<WaveLog>();
    public List<TimeSeriesLog> timeSeriesLogs = new List<TimeSeriesLog>();
    public List<DDAEvent> ddaEvents = new List<DDAEvent>();
    public List<DamageEvent> damageEvents = new List<DamageEvent>();
    public List<HealEvent> healEvents = new List<HealEvent>();

    //Combat Info
    public float totalDamageDealtByPlayer;
    public int numAttacksAttempted;
    public int numAttacksHit;

    //Wave info
    public int enemiesKilledThisWave;
    public int totalEnemiesKilled;



    private void Awake()
    {
        instance = this;
    }





    public void LogAttackAttempt()
{
    numAttacksAttempted++;
}

public void LogAttackHit()
    {
        numAttacksHit++;
    }


public void LogDamageDealt(float damage)
    {
        totalDamageDealtByPlayer += damage;
        
    }


public void LogEnemyKilled()
    {
        enemiesKilledThisWave++;
        totalEnemiesKilled++;
    }




public void ResetStartOfWave()
    {
        enemiesKilledThisWave = 0;
    }


public void SaveWaveLog(int waveNumber)
    {
        WaveLog waveLog = new WaveLog();
        waveLog.waveNumber = waveNumber;
        waveLog.enemiesKilledThisWave = enemiesKilledThisWave;
        waveLogs.Add(waveLog);
    }


    public void LogSnapshot(
    float timeStamp, int waveNumber,
    float playerHealth, float playerHealthPercentage,
    int enemiesAlive, float difficultyMultiplier,
    float healthPickupDropChance
    )
    {
        TimeSeriesLog log = new TimeSeriesLog();
        log.timeStamp = timeStamp;
        log.waveNumber = waveNumber;
        log.playerHealth = playerHealth;
        log.playerHealthPercentage = playerHealthPercentage;
        log.enemiesAlive = enemiesAlive;
        log.enemiesKilledThisWave = enemiesKilledThisWave;
        log.difficultyMultiplier = difficultyMultiplier;
        log.healthPickupDropChance = healthPickupDropChance;

        timeSeriesLogs.Add(log);
    }



    public void LogDDAEvent(
        float timeStamp,
        int waveNumber,
        float damageTaken,
        float healthPercentage,
        float performanceScore,
        float oldDifficulty,
        float newDifficulty
    )
    {
        DDAEvent ddaEvent = new DDAEvent();
        ddaEvent.timeStamp = timeStamp;
        ddaEvent.waveNumber = waveNumber;
        ddaEvent.damageTaken = damageTaken;
        ddaEvent.healthPercentage = healthPercentage;
        ddaEvent.performanceScore = performanceScore;
        ddaEvent.oldDifficulty = oldDifficulty;
        ddaEvent.newDifficulty = newDifficulty;

        ddaEvents.Add(ddaEvent);
    }


    public void LogDamageEvent(float damage, float healthBefore, float healthAfter)
    {
        DamageEvent damageEvent = new DamageEvent();
        damageEvent.timeStamp = Time.time;
        damageEvent.damageAmount = damage;
        damageEvent.playerHealthBefore = healthBefore;
        damageEvent.playerHealthAfter = healthAfter;

        damageEvents.Add(damageEvent);
    }

    public void LogHealEvent(float amount, float healthBefore, float healthAfter)
    {
        HealEvent healEvent = new HealEvent();
        healEvent.timeStamp = Time.time;
        healEvent.healAmount = amount;
        healEvent.healthBefore = healthBefore;
        healEvent.healthAfter = healthAfter;

        healEvents.Add(healEvent);
    }

public void CreateCSV()
    {
        string csvName = $"Log_{DateTime.Now:dd-MM--yyyy-HH--mm--ss}.csv";
        string path = Path.Combine(Application.persistentDataPath, csvName);
        using (StreamWriter csvWriter = new StreamWriter(path))
        {
            csvWriter.WriteLine("Attacks Attempted,Attacks Hit,Total Damage, Enemies killed total");
            
            csvWriter.WriteLine($"{numAttacksAttempted},{numAttacksHit},{totalDamageDealtByPlayer}, {totalEnemiesKilled}");

            csvWriter.WriteLine();

            csvWriter.WriteLine("WaveNumber,EnemiesKilledThisWave");

            foreach (WaveLog waveLog in waveLogs)
            {
                csvWriter.WriteLine($"{waveLog.waveNumber},{waveLog.enemiesKilledThisWave}");
            }

            csvWriter.WriteLine();
            csvWriter.WriteLine("Time Series Log");
            csvWriter.WriteLine("Timestamp,WaveNumber,PlayerHealth,PlayerHealth%,AliveEnemies,EnemiesKilledThisWave,DifficultyMultiplier,HealthPickupDropChance");
            
            foreach (TimeSeriesLog log in timeSeriesLogs)
            {
                csvWriter.WriteLine
                (
                    $"{log.timeStamp:F2}," + $"{log.waveNumber}," +
                    $"{log.playerHealth}," + $"{log.playerHealthPercentage}," +
                    $"{log.enemiesAlive}," + $"{log.enemiesKilledThisWave}," +
                    $"{log.difficultyMultiplier}," + $"{log.healthPickupDropChance}"
                );
            }

            csvWriter.WriteLine();
            csvWriter.WriteLine("Damage Events");
            csvWriter.WriteLine("Timestamp,DamageAmount,HealthBefore,HealthAfter");
            foreach (DamageEvent damageEvent in damageEvents)
            {
                csvWriter.WriteLine(
                    $"{damageEvent.timeStamp:F2}," + $"{damageEvent.damageAmount}," +
                    $"{damageEvent.playerHealthBefore},"+ $"{damageEvent.playerHealthAfter}"
                );
            }

            csvWriter.WriteLine();
            csvWriter.WriteLine("Heal Events");
            csvWriter.WriteLine("Timestamp,HealAMount,HealthBefore,HealthAfter");
            foreach (HealEvent healEvent in healEvents)
            {
                csvWriter.WriteLine(
                    $"{healEvent.timeStamp:F2}," + $"{healEvent.healAmount}," +
                    $"{healEvent.healthBefore},"+ $"{healEvent.healthAfter}"
                );
            }

            csvWriter.WriteLine();
            csvWriter.WriteLine("DDA Events");
            csvWriter.WriteLine("Timestamp,Wave,DamageTaken,Health%,PerformanceScore,OldDifficulty,NewDifficulty");
            foreach (DDAEvent ddaEvent in ddaEvents)
            {
                csvWriter.WriteLine(
                    $"{ddaEvent.timeStamp:F2}," + $"{ddaEvent.waveNumber}," +
                    $"{ddaEvent.damageTaken},"+ $"{ddaEvent.healthPercentage}," +
                    $"{ddaEvent.performanceScore}," + $"{ddaEvent.oldDifficulty},"+
                    $"{ddaEvent.newDifficulty}"
                );
            }




        }
        Debug.Log("CSV saved to: " + path);
    }
    
}





