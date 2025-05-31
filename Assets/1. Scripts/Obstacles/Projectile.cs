using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int damage;
    private Rigidbody2D rb;
    [SerializeField] float launchForce = 5f;
    [SerializeField] float upwardForce = 3f;
    private int dir = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f; 

        Vector2 force = new Vector2(dir * launchForce, upwardForce);
        rb.AddForce(force, ForceMode2D.Impulse);

        Destroy(gameObject, 4f);
    }

    public void SetDirection(int dir)
    {
        this.dir = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStatus player = collision.gameObject.GetComponent<PlayerStatus>();
            player.TakeDamage(damage);
            Debug.Log(player.GetCurrentHealth());
            Destroy(gameObject);
        }
    }
}
