using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private int isRunningHash;
    private int isWallSlideHash;
    private int isWallJumpingHash;
    private int onJumpHash;

    private void Start()
    {
        animator = GetComponent<Animator>();

        // Cache animator parameter hashes for performance
        isRunningHash = Animator.StringToHash("isRunning");
        isWallSlideHash = Animator.StringToHash("isWallSlide");
        isWallJumpingHash = Animator.StringToHash("isWallJumping");
        onJumpHash = Animator.StringToHash("onJump");
    }

    public void SetRunning(bool isRunning)
    {
        animator.SetBool(isRunningHash, isRunning);
    }

    public void SetWallSlide(bool isWallSlide)
    {
        animator.SetBool(isWallSlideHash, isWallSlide);

        // If wall slide ends, clear wall jump flag
        if (!isWallSlide)
            SetWallJumping(false);
    }

    public void SetWallJumping(bool isWallJumping)
    {
        animator.SetBool(isWallJumpingHash, isWallJumping);
    }

    public void TriggerJump()
    {
        animator.ResetTrigger(onJumpHash);
        animator.SetTrigger(onJumpHash);
    }
}
