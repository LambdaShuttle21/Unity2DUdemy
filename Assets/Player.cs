using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] public float moveSpeed = 3.5f;
    [SerializeField] public float jumpForce = 8f;
    public float xInput;
    // We must obtain the components we're using from Unity
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        Jump();
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        bool isMoving;
        if (rb.linearVelocity == Vector2.zero)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }

    public void HandleMovement()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocityY);//(x,y)
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
        }

    }
}
