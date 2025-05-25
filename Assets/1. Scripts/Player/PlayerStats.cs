using UnityEngine;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    [Header("Core Stats")]
    public float maxHealth = 100f;
    public float maxStamina = 50f;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public float staminaRegenRate = 5f;

}
