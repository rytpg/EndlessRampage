using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 1f;


    private Collider2D swordCollider;
    private Vector2 rightLocalOffset;

    private void Awake()
    {
        swordCollider = GetComponent<Collider2D>();
        rightLocalOffset = transform.localPosition;
        swordCollider.enabled = false;
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = rightLocalOffset;
        StatTracker.instance.LogAttackAttempt();

    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition =
            new Vector2(-rightLocalOffset.x, rightLocalOffset.y);
        StatTracker.instance.LogAttackAttempt();
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        HealthManager health = collision.GetComponent<HealthManager>();
        if(health != null)
        {
            StatTracker.instance.LogAttackHit();
            StatTracker.instance.LogDamageDealt(damage);
            health.TakeDamage(damage);
        }

    }
}
