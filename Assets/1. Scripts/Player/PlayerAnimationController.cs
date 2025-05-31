using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetFloat(string anim, float value)
    {
        animator.SetFloat(anim, value);
    }

    public void SetBool(string anim,bool isAnim)
    {
        animator.SetBool(anim,isAnim);
    }

    public void SetTrigger(string anim)
    {
        animator.SetTrigger(anim);
    }
}
