using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement m_playerMovement;
    private PlayerInteract m_playerInteract;

    private void Awake()
    {
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerInteract = GetComponent<PlayerInteract>();
    }

    private void Start()
    {
        m_playerInteract.Init();
    }

    private void Update()
    {
        m_playerMovement.FrameUpdate();
        m_playerInteract.FrameUpdate();
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
