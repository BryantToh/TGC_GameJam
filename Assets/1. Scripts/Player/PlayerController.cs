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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity entity = collision.GetComponent<Entity>();

        if (entity != null)
        {
            entity.DealDamage(GetComponent<PlayerStatus>());
            Debug.Log("Entity detected");
        }
    }
}
