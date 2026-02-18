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

   
    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        Jump();

        if(moveInput > 0 && transform.localScale.x < 0 || moveInput < 0 && transform.localScale.x > 0)
        {
            Flip();
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