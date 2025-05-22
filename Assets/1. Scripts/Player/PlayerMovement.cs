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
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isJumpPressed;

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
        rb.linearVelocity = new Vector2(horizontal * movementSpeed, rb.linearVelocityY);

        if (rb.linearVelocityY < 0)
        {
            rb.gravityScale = gravityMultiplier;
        }
        else if (rb.linearVelocityY > 0 && !isJumpPressed)
        {
            rb.gravityScale = gravityMultiplier * 0.5f;
        }
        else
        {
            rb.gravityScale = 2f;
        }

        if (isFacingRight && horizontal < 0)
            FlipX();
        else if (!isFacingRight && horizontal > 0)
            FlipX();

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            playerAnim.SetRunning(true);
        }
        else
        {
            playerAnim.SetRunning(false);
        }
    }

    private bool IsGrounded() => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    private void FlipX()
    {
        isFacingRight = !isFacingRight;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;

    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpPower);
            isJumpPressed = true;
        }
        // Check if button is half pressed
        if (context.canceled)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * .5f); // Half jump
            isJumpPressed = false;
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
}
