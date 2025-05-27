using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void MoveProjectile(Vector3 targetPos)
    {
        Vector2 dir = (transform.position - targetPos).normalized;
        rb.linearVelocityX = dir.x * speed;
    }
}
