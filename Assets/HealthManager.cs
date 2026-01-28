using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 5f;
    private float currentHealth;

    public UnityEvent onDamage;
    public UnityEvent onDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        onDamage?.Invoke();

        if (currentHealth <= 0f)
            onDeath?.Invoke();
    }

    public float GetHealth() => currentHealth;
}
