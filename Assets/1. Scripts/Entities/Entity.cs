using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int damage = 10;

    public virtual void DealDamage(PlayerStatus player)
    {
        // Deals Damage to player code
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }

}
