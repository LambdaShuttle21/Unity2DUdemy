using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem.XInput;
//pivot: the center of my sprite

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("Attack details")]
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask whatIsEnemy;

    [Header("Movement details")] 
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8f;
    public float xInput;
    private bool canMove = true;
    private bool canJump = true;
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
    public void DamageEnemies()
    {
        // Collision with enemies will be saved in enemyColliders (array, type of Collider2D) only the enemies within the circle, at the next hit the array will reset itself
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsEnemy);// Overlap that will detect anything with the circle
                                                                                                                  // we set up

        foreach (Collider2D enem in enemyColliders)
        {
            enem.GetComponent<Enemy>().TakeDamage();
        }
    }
    public void EnableMovementAndJump(bool enable)
    {
        canMove = enable;
        canJump = enable;
    }
    private void HandleAnimations()
    {
        bool isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.01f;//(0,0) si velocidad mayor a 0, isMoving = true

        // Animator parameters (blend trees)
        animator.SetFloat("xVelocity", rb.linearVelocity.x);
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetBool("isGrounded", isGrounded);//this sets the movement animation correctly, if you move (isMoving = true) then
        // the animator transition of "playerMove" will execute, therefore nothing will happen (playerIdle will execute eternally haha)
        // sprite flip
        if (rb.linearVelocity.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (rb.linearVelocity.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))

            TryToJump();

        if (Input.GetKeyDown(KeyCode.Mouse0))

            TryToAttack();

    }

    private void TryToAttack()
    {
        if (isGrounded)
        {
            animator.SetTrigger("attack");
        }
    }
    private void HandleMovement()
    {
        if (canMove == true)//this cames from PlayerAnimationEvents -> AE_DisableMovementAndJump
        {
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocityY);//(x,y)
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);//(x,y)
        }

    }
    private void TryToJump()
    {
        if (isGrounded && canJump)// canJump cames from PlayerAnimationEvents -> DisableMovementAndJump
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

    }

    private void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);//
        //(initial position, direction (0,-1), distance to ground, which layer is meant to be ground)
    }

    private void OnDrawGizmos()// Imaginary line meant to touch the ground and check whether isGrounded = true or false, this only serves as reference for the Raycast
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, 0.02f);
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);// (My initial position, the direction of the
        // Gizmos.DrawLine, I will sum up the line distance and direction (vector) to my initial position 

        //Gizmos for enemy
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
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
