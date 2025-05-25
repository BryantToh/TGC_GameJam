using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement m_playerMovement;

    private void Awake()
    {
        m_playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        m_playerMovement.UpdateTransform();
    }
}
