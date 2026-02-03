using UnityEngine;

public class DeathHandler : MonoBehaviour
{

private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void Die()
    {
        animator.SetTrigger("Die");
    }

    public void DestroySelf()
    {
        Destroy(transform.root.gameObject);
    }

}
