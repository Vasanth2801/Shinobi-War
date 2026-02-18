using UnityEngine;
public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private int facingDirection = 1;   

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [Header("GroundCheck Settings")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isGrounded;

    [Header("Inputs")]
    [SerializeField] float moveInput;

    [Header("Attack Settings")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask playerLayer;

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        Jump();

        if(moveInput > 0 && transform.localScale.x < 0 || moveInput < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Attack();
        }

        HandleAnimations();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveInput * speed,rb.linearVelocity.y);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, checkRadius, playerLayer);

        foreach(Collider2D hit in hitEnemies)
        {
            EnemyHealth eh = hit.GetComponent<EnemyHealth>();
            if(eh != null )
            {
                eh.TakeDamage(10);
            }
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
    }

    void HandleAnimations()
    {
        bool isMoving = Mathf.Abs(moveInput) > 0.1f && isGrounded;

        animator.SetBool("isIdling", !isMoving && isGrounded);
        animator.SetBool("isRunning",isMoving && isGrounded);
        animator.SetBool("isJumping", rb.linearVelocity.y > 0);
    }
}