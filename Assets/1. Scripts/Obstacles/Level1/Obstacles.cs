using System.Collections;
using UnityEngine;

public class Obstacles : Entity
{
    public bool canKnockback;
    private GameObject player;
    private Rigidbody2D rb;
    private PlayerStatus status;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        status = player.GetComponent<PlayerStatus>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.collider.GetComponent<Rigidbody2D>();
            PlayerStatus playerStatus = collision.collider.GetComponent<PlayerStatus>();

            if (playerRb != null && playerStatus != null)
            {
                if (canKnockback)
                {
                    ApplyKnockBack(playerRb, transform.position);
                    DealDamage(playerStatus);
                }
            }
        }
    }
}
