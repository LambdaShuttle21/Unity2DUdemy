using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem.XInput;
//pivot: the center of my sprite

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator animator;
    protected Collider2D col;
    protected SpriteRenderer sr;

    [Header("Health")]
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int currentHealth;
    [SerializeField] private Material damageMaterial;
    [SerializeField] private float damageFeedbackDuration = .2f;
    private Coroutine damageFeedbackCoroutine;// Active coroutine

    [Header("Attack details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask whatIsTarget;
    protected bool isAttacking;

    protected int facingDir = 1;//for the enemy's speed
    protected bool canMove = true;
    //public bool facingRight = true;

    [Header("Collision details")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] protected bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;
    // We must obtain the components we're using from Unity
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        currentHealth = maxHealth;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleCollision();

        HandleMovement();
        HandleAnimations();
        //HandleFlip();
    }

    public void DamageTargets()
    {
        // Collision with enemies will be saved in enemyColliders (array, type of Collider2D) only the enemies within the circle, at the next hit the array will reset itself
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsTarget);// Overlap that will detect anything with the circle
                                                                                                                   // we set up

        foreach (Collider2D enem in enemyColliders)
        {
            Entity entityTarget = enem.GetComponent<Entity>();// I get my component through foreach loop (enemies in the array, type of Entity)
            entityTarget.TakeDamage();// Health logic
        }
    }

    private void TakeDamage()
    {
        currentHealth = currentHealth - 1;
        PlayDamageFeedback();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void PlayDamageFeedback() // It defines wether a coroutine is
                                      // active or not to stop it and exec a new one or just exec a new one
    {
        if (damageFeedbackCoroutine != null) // active coroutine
        {
            StopCoroutine(damageFeedbackCoroutine); // we stop
        }

        damageFeedbackCoroutine = StartCoroutine(DamageFeedbackCo()); // Start a new one and save it in my private Coroutine variable
        // DamageFeedbackCo(); starts the coroutine
    }

    private IEnumerator DamageFeedbackCo()
    {
        //pa que no pete sino hay spriterenderer
        if (sr == null)
        {
            yield break;
        }

        Material originalMaterial = sr.material;

        sr.material = damageMaterial;// We assign our damage material
        // to our current material so everytime we get hit the coroutine
        // resets from the start instead of having multiple coroutines

        yield return new WaitForSeconds(damageFeedbackDuration); // we assign
        // the coroutine duration

        // Then we return the material to its original state
        sr.material = originalMaterial;
    }
    protected virtual void Die()// Physics for die method (no animations yet)
    {
        animator.enabled = false;
        col.enabled = false;

        rb.gravityScale = 12; // should fall asap
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15);// it goes up and falls too fast, like bouncing

        Destroy(gameObject, 3);
    }

    public virtual void EnableMovement(bool enable)
    {
        canMove = enable;
        //canJump = enable;
    }
    protected virtual void HandleAnimations()
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
            transform.localScale = new Vector3(-1, 1, 1);//I just flip the x axis if my speed is a negative one
    }


    protected virtual void HandleAttack()
    {
        if (isGrounded)
        {
            animator.SetTrigger("attack");
        }
    }
    protected virtual void HandleMovement()
    {
    }


    protected virtual void HandleCollision()
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

        if (attackPoint != null)
        {
            //Gizmos for enemy
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }

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
