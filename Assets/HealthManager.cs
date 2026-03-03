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

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if(dead) return;
        currentHealth -= amount;

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
        //Makes sure it doesnt heal over max heal
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        onHealthChange?.Invoke();
    }


    public float GetHealth() => currentHealth;
}
