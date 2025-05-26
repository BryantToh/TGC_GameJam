using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetRunning(bool isRunning)
    {
        animator.SetBool("isRunning", isRunning);
    }

    public void SetWallSlide(bool isWallSlide)
    {
        animator.SetBool("isWallSlide", isWallSlide);
    }
    public void TriggerJump()
    {
        animator.ResetTrigger("onJump");
        animator.SetTrigger("onJump");
    }
}
