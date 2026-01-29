using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.3f;

    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if(playerObj != null)
        {
            player = playerObj.transform;
        }

        
    }

    void Update()
    {
        if(isDead || player == null)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isMoving", false);
            return;
        }


        Vector2 directionToPlayer = (Vector2)player.position - rb.position;
        float distance = directionToPlayer.magnitude;
        
        if(distance <= collisionOffset)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isMoving", false);
            return;
        }

        Vector2 direction = directionToPlayer.normalized;
        rb.linearVelocity = direction * moveSpeed;
        animator.SetBool("isMoving", true);

        if(spriteRenderer != null)
        {
            spriteRenderer.flipX = (direction.x < 0);
        }

    }

    public void OnDeath()
    {
        isDead = true;
        //Incase enemy moves for 1 frame, before update() hits
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("isMoving", false);
        
    }



}
