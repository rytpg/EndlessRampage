using UnityEngine;

public class EnemyContactAttack : MonoBehaviour
{
    public float damage = 1f;
    public float cooldown = 0.5f;
    
    private float cooldownTimer = 0f;

    private Collider2D attackCollider;

    void Start()
    {
        attackCollider = GetComponent<Collider2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime; // time since last frame
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (cooldownTimer > 0f) return;
        if (!collision.CompareTag("Player")) return;

        HealthManager healthManager  = collision.GetComponent<HealthManager>();
        if (healthManager != null)
        {
            healthManager.TakeDamage(damage);
            cooldownTimer = cooldown;
        }
    }

    public void DisableAttack()
    {
        attackCollider.enabled = false;
        enabled = false;
    }
}

