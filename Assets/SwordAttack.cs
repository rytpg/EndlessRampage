using UnityEngine;

public class SwordAttack : MonoBehaviour
{
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
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition =
            new Vector2(-rightLocalOffset.x, rightLocalOffset.y);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

        }
    }
}
