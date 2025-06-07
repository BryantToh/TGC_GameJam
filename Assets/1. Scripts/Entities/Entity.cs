using System.Collections;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int damage = 10;
    private float knockbackForce = 5f;
    private float knockbackDuration = 0.15f;
    public bool isKnockedBack = false;

    public virtual void DealDamage(PlayerStatus player)
    {
        // Deals Damage to player code
        if (player != null)
        {
            player.TakeDamage(damage);
            AudioManager.instance.PlaySFX("damage", 0.5f);
        }
    }
    public virtual void ApplyKnockBack(Rigidbody2D rb, Vector2 obstaclePos)
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
