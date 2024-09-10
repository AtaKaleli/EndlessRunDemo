using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;


    [Header("Player Inputs")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;


    [Header("Collision Checks - Ground")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;




    private bool playerUnlocked;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (playerUnlocked)
            Move();

        InputChecks();
        CollisionChecks();
        AnimationController();

    }

    private void InputChecks()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            playerUnlocked = true;


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump();
    }

    private void AnimationController()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);

    }



    private void Move()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance, groundCheck.position.z));
    }



}
