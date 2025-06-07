using UnityEngine;

public class MenuTransition : MonoBehaviour
{
    [SerializeField]
    Animator m_transitionAnimator;
    private void Start()
    {
        OpenAnimation();
    }
    public void OpenAnimation()
    {
        m_transitionAnimator.SetTrigger("Open");
    }
}
