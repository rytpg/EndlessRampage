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

    public List<WaveLog> waveLogs = new List<WaveLog>();


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

public void CreateCSV()
    {
        string csvName = $"Log_{DateTime.Now:dd-MM--yyyy-HH--mm--ss}.csv";
        string path = Path.Combine(Application.persistentDataPath, csvName);
        using (StreamWriter csvWriter = new StreamWriter(path))
        {
            csvWriter.WriteLine("Attacks Attempted,Attacks Hit,Total Damage, Enemies killed total");
            
            csvWriter.WriteLine($"{numAttacksAttempted},{numAttacksHit},{totalDamageDealtByPlayer}, {totalEnemiesKilled}");

            csvWriter.WriteLine();

            foreach (WaveLog waveLog in waveLogs)
            {
                csvWriter.WriteLine($"{waveLog.waveNumber},{waveLog.enemiesKilledThisWave}");
            }
        }
        Debug.Log("CSV saved to: " + path);
    }
    
}





