using System.Collections;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] int objDmg;
    private float knockbackForce = 50f;
    private float knockbackDuration = 0.15f;
    public bool isKnockedBack = false;
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
            DealDmg();
        }
    }
    private void DealDmg()
    {
        ApplyKnockBack();
        status.TakeDamage(objDmg);
    }
    private void ApplyKnockBack()
    {
        if (isKnockedBack)
            return;

        isKnockedBack = true;
        Vector2 knockbackDirection = (rb.position - obstaclePos).normalized;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(EndKnockback());
    }
    private IEnumerator EndKnockback()
    {
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
    }
}
