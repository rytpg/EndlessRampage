using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 5f;
    private float currentHealth;
    private bool dead = false;

    public UnityEvent onDamage;
    public UnityEvent onDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if(dead) return;
        currentHealth -= amount;
        onDamage?.Invoke();

        if (currentHealth <= 0f)
        {
            dead = true;
            onDeath?.Invoke();
        }
    }

    public float GetHealth() => currentHealth;
}
