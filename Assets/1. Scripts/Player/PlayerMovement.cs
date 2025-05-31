using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //[Header("PlayerInput")]
    //public InputSO playerInput;

    //[Header("Movement Var")]
    //public float movementSpeed;
    //public float sprintSpeed = 7f;
    //private float horizontal;

    //[Header("Jump Var")]
    //public float jumpPower = 8f;
    //public float gravityMultiplier;
    //public float staminaCostPerJump = 10f;
    //private bool isJumpPressed;
    //private int jumpCount = 0;
    //private int maxJumpCount = 2;

    //[Header("Ground Check")]
    //public Transform groundCheck;
    //public LayerMask groundLayer;

    //[Header("Wall Check")]
    //public Transform wallCheck;
    //public LayerMask wallLayer;

    //[Header("Wall Action Var")]
    //public float wallSlideSpeed = 7f;
    //private bool isWallSlide = false;

    //[Header("Wall Jump")]
    //private bool isWallJump = false;
    //private float wallJumpDir;
    //float wallJumpTime = .5f;
    //float wallJumpTimer;
    //public Vector2 wallJumpPower = new Vector2(5f, 8f);

    //private Rigidbody2D rb;
    //private PlayerAnimationController playerAnim;
    //private PlayerStatus playerStatus;
    //private bool isFacingRight = true;

    //private void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    playerAnim = GetComponent<PlayerAnimationController>();
    //    playerStatus = GetComponent<PlayerStatus>();
    //}
    //public void FrameUpdate()
    //{
    //    float currentSpeed = movementSpeed;

    //    if (horizontal != 0 && playerInput.GetKey(playerInput.sprintKey))
    //    {
    //        currentSpeed += sprintSpeed;
    //    }

    //    rb.linearVelocity = new Vector2(horizontal * currentSpeed, rb.linearVelocity.y);
    //    FlipX();

    //    if (rb.linearVelocity.y < 0)
    //        rb.gravityScale = gravityMultiplier;
    //    else if (rb.linearVelocity.y > 0 && !isJumpPressed)
    //        rb.gravityScale = gravityMultiplier * 0.5f;
    //    else
    //        rb.gravityScale = 2f;

    //    // Reset jump count
    //    if (IsGrounded())
    //        jumpCount = 0;

    //    if (Mathf.Abs(horizontal) > 0.1f)
    //        playerAnim.SetRunning(true);
    //    else
    //        playerAnim.SetRunning(false);


        

    //    WallSlide();
    //    WallJump();
    //}
    //private void FlipX()
    //{
    //    if (isFacingRight && horizontal < 0 || !isFacingRight && horizontal > 0)
    //    {
    //        isFacingRight = !isFacingRight;
    //        Vector2 localScale = transform.localScale;
    //        localScale.x *= -1f;
    //        transform.localScale = localScale;
    //    }
        
    //}
    //private void CancelWallJump()
    //{
    //    isWallJump = false;
    //}
    //private void WallSlide()
    //{
    //    if (!IsGrounded() && IsOnWall() && horizontal != 0)
    //    {
    //        isWallSlide = true;
    //        rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
    //        isFacingRight = !isFacingRight;
    //        Vector2 localScale = transform.localScale;
    //        localScale.x *= -1f;
    //        transform.localScale = localScale;
    //    }
    //    else
    //    {
    //        isWallSlide = false;
    //    }
    //    playerAnim.SetWallSlide(isWallSlide);
    //}
    //private void WallJump()
    //{
    //    if (isWallSlide)
    //    {
    //        isWallJump = false;
    //        wallJumpDir = -transform.localScale.x;
    //        wallJumpTimer = wallJumpTime;

    //        CancelInvoke(nameof(CancelWallJump));
    //    }
    //    else if(wallJumpTimer > 0f)
    //    {
    //        wallJumpTimer -= Time.deltaTime;
    //    }
    //}
    //private bool IsGrounded() => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    //private bool IsOnWall() => Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    //public void Jump(InputAction.CallbackContext context)
    //{
    //    if (jumpCount < maxJumpCount/* && playerStatus.GetCurrentStamina() >= staminaCostPerJump*/)
    //    {
    //        if (context.performed)
    //        {
    //            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
    //            isJumpPressed = true;
    //            jumpCount++;
    //            playerStatus.UseStamina(staminaCostPerJump);

    //            playerAnim.TriggerJump();
    //        }
    //        // Check if button is half pressed
    //        else if (context.canceled)
    //        {
    //            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * .5f); // Half jump
    //            isJumpPressed = false;
    //            jumpCount++;
    //        }
    //    }

    //    // Wall jump
    //    if (context.performed && wallJumpTimer > 0f)
    //    {
    //        isWallJump = true;
    //        rb.linearVelocity = new Vector2(wallJumpDir * wallJumpPower.x, wallJumpPower.y);

    //        if (transform.localScale.x != wallJumpDir)
    //        {
    //            isFacingRight = !isFacingRight;
    //            Vector2 localScale = transform.localScale;
    //            localScale.x *= -1f;
    //            transform.localScale = localScale;
    //        }


    //        Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f);
    //    }
        
    //}
    //public void Move(InputAction.CallbackContext context)
    //{
    //    horizontal = context.ReadValue<Vector2>().x;
    //}
}
