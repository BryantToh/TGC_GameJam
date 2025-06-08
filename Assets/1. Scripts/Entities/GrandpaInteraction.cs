using UnityEngine;

public class GrandpaInteraction : MonoBehaviour, IInteractable
{
    public bool isStanding = false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("IsStanding", isStanding);     
    }
    public void Interact()
    {
        LevelManager.Instance.SetCompleteLevel(true);
    }
}
