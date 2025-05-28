using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int damage;
    private Rigidbody2D rb;
    private float speed = 5f;
    private int dir = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(dir * speed, 0);
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
