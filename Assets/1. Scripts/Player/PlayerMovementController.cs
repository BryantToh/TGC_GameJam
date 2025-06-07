using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovementController : MonoBehaviour
{ 
    [Header("PlayerInput")]
    public InputSO playerInput;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 7f;
    public float staminaCostPerSecond = 10f;
    float horizontalMovement;

    [Header("Flip Sprite")]
    private bool isFacingRight = true;

    [Header("Jumping")]
    public float jumpPower = 6f;
    public int maxJumps = 2;
    private int jumpsRemaining;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.49f, 0.03f);
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Wall Check")]
    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(0.49f, 0.03f);
    public LayerMask wallLayer;

    [Header("Wall Movement")]
    public float wallSlideSpeed = 2f;
    private bool isWallSliding;

    [Header("Wall Jump")]
    private bool isWallJumping;
    private float wallJumpDirection;
    private float wallJumpTime = 0.5f;
    private float wallJumpTimer;
    public Vector2 wallJumpPower = new Vector2(5f, 6f);

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallGravityMult = 2f;

    private Rigidbody2D rb;
    private Animator playerAnim;
    private SpriteRenderer spriteRenderer;
    private PlayerStatus playerStatus;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerStatus = GetComponent<PlayerStatus>();
    }
    public void FrameUpdate()
    {
        ProcessWallSlide();
        ProcessWallJump();
        ProcessGravity();
        GroundCheck();

        bool canSprint = playerStatus.GetCurrentStamina() > 0.1;

        float currentSpeed = moveSpeed;

        if (horizontalMovement != 0 && playerInput.GetKey(playerInput.sprintKey))
        {
            if (canSprint)
            {
                currentSpeed += sprintSpeed;
                playerStatus.UseStamina(staminaCostPerSecond * Time.deltaTime);
                playerStatus.SetSprinting(true);
            }
            else
                playerStatus.SetSprinting(false);
        }
        else
            playerStatus.SetSprinting(false);

        if (Mathf.Abs(horizontalMovement) > 0.1f)
            playerAnim.SetBool("IsWalk", true);
        else
            playerAnim.SetBool("IsWalk", false);


        if (!isWallJumping && !isWallSliding)
        {
            rb.linearVelocity = new Vector2(horizontalMovement * currentSpeed, rb.linearVelocity.y);
            FlipX();
        }

        playerAnim.SetFloat("yVelocity", rb.linearVelocityY);
        playerAnim.SetBool("isWallSliding", isWallSliding);

        playerAnim.SetBool("isJump", !isGrounded && rb.linearVelocity.y > 0.1f);


    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
        
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining > 0)
        {
            if (context.performed)
            {
                //Hold down jump button = full height
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                jumpsRemaining--;

                playerAnim.SetBool("isJump", true);
            }
            else if (context.canceled && rb.linearVelocity.y > 0)
            {
                //Light tap of jump button = half the height
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
                jumpsRemaining--;

                playerAnim.SetBool("isJump", true);
            }
        }

        if (context.performed && wallJumpTimer > 0f)
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            wallJumpTimer = 0f;

            playerAnim.SetBool("isJump", true);

            if (transform.localScale.x != wallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale; 
            }

            Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f);
        }
    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer)) //checks if set box overlaps with ground
        {
            jumpsRemaining = maxJumps;
            isGrounded = true;

            playerAnim.SetBool("isJump", false);
        }
        else
            isGrounded = false;
    }

    private bool WallCheck()
    {
        return Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer);
    }

    private void ProcessWallSlide()
    {
        if (!isGrounded && WallCheck() && horizontalMovement != 0)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -wallSlideSpeed));

        }
        else
            isWallSliding = false;
    }

    private void ProcessWallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;

            CancelInvoke(nameof(CancelWallJump));
        }
        else if(!isWallSliding && wallJumpTimer > 0f)
        {
            wallJumpTimer -= Time.deltaTime;
        }
    }

    private void CancelWallJump()
    {
        isWallJumping = false;
    }

    private void ProcessGravity()
    {
        //falling gravity
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallGravityMult; //fall faster and faster
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed)); //max fall speed
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    private void FlipX()
    {
        if (isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale; 
        }
    }

    private void OnDrawGizmosSelected()
    {
        //Ground check visual
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
