using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 5f;
    private float currentHealth;
    private bool dead = false;

    public UnityEvent onDamage;
    public UnityEvent onDeath;
    public UnityEvent onHealthChange;

    public bool isPlayer = false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if(dead) return;

        float healthBefore = currentHealth;

        currentHealth -= amount;
        currentHealth = Mathf.Max(0f, currentHealth);

        if(isPlayer){
            StatTracker.instance?.LogDamageEvent(amount, healthBefore, currentHealth);
        }

        onDamage?.Invoke();
        onHealthChange?.Invoke();

        if (currentHealth <= 0f)
        {
            dead = true;
            onDeath?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        if(dead) return;
        float healthBefore = currentHealth;
        //Makes sure it doesnt heal over max heal
        float newHealth = Mathf.Min(maxHealth, currentHealth + amount);
        // this makes sure we get the actual heal amount since if the player
        // is full health it might say +5 or whatever but it didnt actual heal that amount
        float actualHealing = newHealth - currentHealth;

        // currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        currentHealth = newHealth;
        if(isPlayer){
        StatTracker.instance.LogHealEvent(actualHealing,healthBefore,currentHealth);
        }
        
        onHealthChange?.Invoke();
    }


    public float GetHealth() => currentHealth;
}
