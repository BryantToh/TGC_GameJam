using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Var")]
    public float movementSpeed;
    private float horizontal;

    [Header("Jump Var")]
    public float jumpPower;
    public float gravityMultiplier;
    private bool isJumpPressed;
    private int jumpCount = 0;
    private int maxJumpCount = 2;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Wall Check")]
    public Transform wallCheck;
    public LayerMask wallLayer;

    [Header("Wall Action Var")]
    private float wallSlideSpeed = 7f;
    private bool isWallSlide = false;

    private Rigidbody2D rb;
    private PlayerAnimationController playerAnim;
    private bool isFacingRight = true;

    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimationController>();
    }

    public void UpdateTransform()
    {
        rb.linearVelocity = new Vector2(horizontal * movementSpeed, rb.linearVelocity.y);

        if (rb.linearVelocity.y < 0)
            rb.gravityScale = gravityMultiplier;
        else if (rb.linearVelocity.y > 0 && !isJumpPressed)
            rb.gravityScale = gravityMultiplier * 0.5f;
        else
            rb.gravityScale = 2f;

        // Reset jump count
        if (IsGrounded())
            jumpCount = 0;


        // Set sprite facing direction
        if (isFacingRight && horizontal < 0)
            FlipX();
        else if (!isFacingRight && horizontal > 0)
            FlipX();

        if (Mathf.Abs(horizontal) > 0.1f)
            playerAnim.SetRunning(true);
        else
            playerAnim.SetRunning(false);
        
        WallSlide();
    }
        
    private bool IsGrounded() => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    
    private bool IsOnWall() => Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    private void FlipX()
    {
        isFacingRight = !isFacingRight;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;

    }

    
    private void WallSlide()
    {
        if (!IsGrounded() && IsOnWall() && horizontal != 0)
        {
            isWallSlide = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
        }
        else
        {
            isWallSlide = false;
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpCount < maxJumpCount)
        {
            if (context.performed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                isJumpPressed = true;
                jumpCount++;
            }
            // Check if button is half pressed
            else if (context.canceled)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * .5f); // Half jump
                isJumpPressed = false;
                jumpCount++;
            }
        }
        
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
}
