using UnityEngine;

public class HealthPickup : MonoBehaviour

{
    public float healAmount = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthManager healthManager = collision.GetComponent<HealthManager>();
            if(healthManager != null)
            {
                healthManager.Heal(healAmount);
                Destroy(gameObject);
            }
        }
        
    }
}
