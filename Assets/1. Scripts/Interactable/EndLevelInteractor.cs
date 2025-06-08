using UnityEngine;

public class EndLevelInteractor : MonoBehaviour, IInteractable
{
    bool hasInteracted = false;
    public void Interact()
    {
        if (hasInteracted)
        {
            Debug.Log("End level");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasInteracted = true;
        }
    }
}
