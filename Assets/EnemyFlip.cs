using Pathfinding;
using UnityEngine;

public class EnemyFlip : MonoBehaviour
{
    private AIPath aiPath;
    public Transform visuals;
    private Animator animator;

    private bool isDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aiPath = GetComponent<AIPath>();
        animator = GetComponentInChildren<Animator>();
    }

    

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = visuals.localScale;
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            scale.x = 1f;
        }
        else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            scale.x = -1f;
        }
        
        visuals.localScale = scale;
        
    }

    public void OnDeath()
    {
        isDead = true;
        animator.SetBool("isMoving", false); 
        aiPath.canMove = false;
        aiPath.canSearch = false;
        
    }
}
