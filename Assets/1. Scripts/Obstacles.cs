using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private int objDmg;
    private PlayerStats playerStats;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DealDmg();
        }
    }
    private void DealDmg()
    {

    }
}
