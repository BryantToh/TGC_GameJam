using System.Collections;
using UnityEngine;

public class Obstacles : Entity
{
    private Vector2 obstaclePos;
    private GameObject player;
    private Rigidbody2D rb;
    private PlayerStatus status;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        status = player.GetComponent<PlayerStatus>();
        obstaclePos = new Vector2(transform.position.x, transform.position.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ApplyKnockBack(rb, obstaclePos);
            DealDamage(status);
        }
    }
}
