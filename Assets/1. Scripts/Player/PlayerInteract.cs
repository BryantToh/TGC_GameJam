using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private GameObject buttonDisplay;
    [SerializeField] private InputSO playerInput;
    [SerializeField] private float m_interactRadius = 1f;

    public void Init()
    {
        buttonDisplay.SetActive(false);
    }
    public void FrameUpdate()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, m_interactRadius);

        bool foundInteractable = false;

        foreach (var collider in col)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            Key key = collider.GetComponent<Key>();

            if (key != null)
            {
                int id = key.keyId;
                LevelManager.Instance.GetMemory().CollectKey(id);
                collider.gameObject.SetActive(false);
                AudioManager.instance.PlaySFX("interact", 0.5f);
            }

            if (interactable != null)
            {
                foundInteractable = true;
                buttonDisplay.SetActive(true);

                if (playerInput.GetKey(playerInput.interactKey))
                {
                    AudioManager.instance.PlaySFX("interact", 0.5f);
                    interactable.Interact();
                    break;
                }
            }
        }

        if (!foundInteractable)
        {
            buttonDisplay.SetActive(false);
        }
    }
}
