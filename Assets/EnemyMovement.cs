using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.3f;

    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField] private Transform visuals;
    private int facingDirection = 1; // 1 = right, -1 = left

    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

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

        if(direction.x < 0 && facingDirection != -1)
        {
            facingDirection = -1;
            Vector3 s = visuals.localScale;
            s.x = -1;
            visuals.localScale = s;
        }
        else if(direction.x > 0 && facingDirection != 1)
        {
            facingDirection = 1;
            Vector3 s = visuals.localScale;
            s.x = 1;
            visuals.localScale = s;
            
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
