using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem.XInput;
//pivot: the center of my sprite

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("Movement details")]

    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8f;
    public float xInput;
    //public bool facingRight = true;

    [Header("Collision details")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;
    // We must obtain the components we're using from Unity
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleCollision();
        HandleInput();
        HandleMovement();
        HandleAnimations();
        //HandleFlip();
    }

    private void HandleAnimations()
    {
        bool isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.01f;//(0,0) si velocidad mayor a 0, isMoving = true
        animator.SetBool("isMoving", isMoving);//this sets the movement animation correctly, if you move (isMoving = true) then
        // the animator transition of "playerMove" will execute, therefore nothing will happen (playerIdle will execute eternally haha)
        // sprite flip
        if (rb.linearVelocity.x != 0)
        {
            spriteRenderer.flipX = rb.linearVelocity.x < 0;
        }
    }
    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocityY);//(x,y)
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        //(initial position, direction (0,-1), 
    }

    private void OnDrawGizmos()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, 0.02f);
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);// (My initial position, the direction of the
        // Gizmos.DrawLine, I will sum up the line distance and direction (vector) to my initial position 
    }
    //private void HandleFlip()
    //{
    //    if (rb.linearVelocity.x > 0 && facingRight == false)
    //    {
    //        Flip();
    //    }
    //    else if (rb.linearVelocity.x < 0 && facingRight == true)
    //    {
    //        Flip();
    //    }
    //}

    //private void Flip()
    //{
    //    transform.Rotate(0, 180, 0);
    //    facingRight = !facingRight;
    //}
}
