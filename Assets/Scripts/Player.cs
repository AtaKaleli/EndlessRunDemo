using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;


    [Header("Player Inputs")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float slidingMoveSpeed;
    private bool playerUnlocked;
    private bool canDoubleJump = true;


    [Header("Collision Checks - Ground")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    [Header("Collision Checks - UpperGround")]
    [SerializeField] private Transform upperGroundCheck;
    private bool isUpperGroundDetected;

    [Header("Collision Checks - Wall")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Vector2 wallCheckSize;
    private bool isWallDetected;

    [Header("Ledge Info")]
    [HideInInspector] public bool isLedgeDetected;
    [SerializeField] private Vector3 offset1; // offset of climb begin position
    [SerializeField] private Vector3 offset2; // offset of climb done position

    private Vector3 climbBegunPosition; // the position where we start to climb
    private Vector3 climbOverPosition; // the position when we done climbing

    private bool canGrabLedge = true;
    private bool canClimb;

    [Header("Slide Ability")]
    [SerializeField] private float slideTimer;
    private float slideTimeCounter;
    private bool isSliding;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        slideTimeCounter -= Time.deltaTime;

        if (slideTimeCounter < 0 && !isUpperGroundDetected)
        {
            isSliding = false;
        }



        if (playerUnlocked)
            Move();

        if (isGrounded)
            canDoubleJump = true;





        CheckForLedge();
        InputChecks();
        CollisionChecks();
        AnimationController();

    }

    private void InputChecks()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            playerUnlocked = true;

        if (Input.GetKeyDown(KeyCode.LeftShift))
            SlideController();

        if (Input.GetKeyDown(KeyCode.Space))
            JumpController();



    }

    private void AnimationController()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("canDoubleJump", canDoubleJump);
        anim.SetBool("isSliding", isSliding);
        anim.SetBool("canClimb", canClimb);
    }

    private void LedgeClimbOver()
    {
        canClimb = false;
        transform.position = climbOverPosition;
        Invoke("AllowLedgeGrab",.1f);

    }

    private void AllowLedgeGrab()
    {
        canGrabLedge = true;
    }

    private void CheckForLedge()
    {
        if (isLedgeDetected && canGrabLedge)
        {
            canGrabLedge = false;
          
            climbBegunPosition = transform.position + offset1;
            climbOverPosition = transform.position + offset2;

            canClimb = true;
        }

        if (canClimb)
        {
            transform.position = climbBegunPosition;
        }
    }

    private void SlideController()
    {
        if (!isSliding)
        {
            isSliding = true;
            slideTimeCounter = slideTimer;
        }

    }


    private void Move()
    {

        if (!isSliding && !isWallDetected)
        {
            print("keep move");
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else if (isWallDetected)
        {
            print("stopped");
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            
            rb.velocity = new Vector2(slidingMoveSpeed, rb.velocity.y);
        }


    }

    private void Jump(float forceMultiplier)
    {
        if(!isSliding)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * forceMultiplier);
    }

    private void JumpController()
    {
        if (isGrounded)
        {
            Jump(1f);

        }
        else if (canDoubleJump)
        {
            canDoubleJump = false;
            Jump(0.75f);

        }

    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

        //BoxCast is used instead of raycast. This is because we need to be sure that there will be no problem with wall checks. We wont detect wall if we use a straight line due to level of walls.
        isWallDetected = Physics2D.BoxCast(wallCheck.position, wallCheckSize, 0, Vector2.zero, 0, whatIsGround);
        
        isUpperGroundDetected = Physics2D.Raycast(groundCheck.position, Vector2.up, groundCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance, groundCheck.position.z));
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y + groundCheckDistance, groundCheck.position.z));
        Gizmos.DrawWireCube(wallCheck.position, wallCheckSize);
    }



}
