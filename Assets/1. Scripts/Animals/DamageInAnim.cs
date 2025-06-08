using UnityEngine;

public class DamageInAnim : MonoBehaviour
{
    Animal animal;
    private void Start()
    {
        animal = GetComponent<Animal>();
    }

    public void Hitplayer()
    {
        animal.DealDamageToPlayer();
    }
}
