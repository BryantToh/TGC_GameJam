using UnityEngine;

public class GrandpaInteraction : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        LevelManager.Instance.SetCompleteLevel(true);
    }
}
