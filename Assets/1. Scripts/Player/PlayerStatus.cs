using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private PlayerStats stats;

    private float currentHealth;
    private float currentStamina;

    public delegate void OnStatusChanged(float current, float max);
    public event OnStatusChanged onHealthChanged;
    public event OnStatusChanged onStaminaChanged;

    private float lastStaminaUseTime; // Track the time when stamina was last used
    public float staminaRegenDelay = 2f; // Delay before starting regen after exhaustion

    private bool isSprinting;

    public void SetSprinting(bool sprinting)
    {
        isSprinting = sprinting;
    }

    public bool IsSprinting()
    {
        return isSprinting;
    }

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        if (stats == null)
            Debug.LogError("PlayerStats component not found!");
    }

    private void Start()
    {
        currentHealth = stats.maxHealth;
        currentStamina = stats.maxStamina;

        onHealthChanged?.Invoke(currentHealth, stats.maxHealth);
        onStaminaChanged?.Invoke(currentStamina, stats.maxStamina);
    }

    private void Update()
    {
        // Test damage/stamina with key input
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(10f);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            UseStamina(10f);
        }

        RegenerateStamina();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, stats.maxHealth);
        onHealthChanged?.Invoke(currentHealth, stats.maxHealth);
        if (currentHealth <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, stats.maxHealth);
        onHealthChanged?.Invoke(currentHealth, stats.maxHealth);
    }

    public void UseStamina(float amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            onStaminaChanged?.Invoke(currentStamina, stats.maxStamina);
            lastStaminaUseTime = Time.time; // Record the time when stamina was used
        }
        else
        {
            Debug.Log("Not enough stamina!");
        }
    }

    private void RegenerateStamina()
    {
        // Only start regenerating if delay has passed since last stamina use
        if (Time.time >= lastStaminaUseTime + staminaRegenDelay)
        {
            currentStamina += stats.staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, stats.maxStamina);
            onStaminaChanged?.Invoke(currentStamina, stats.maxStamina);
        }
    }


    private void Die()
    {
        Debug.Log("Player has died.");
        // Add your death logic here
    }

    public float GetCurrentHealth() => currentHealth;
    public float GetMaxHealth() => stats.maxHealth;
    public float GetCurrentStamina() => currentStamina;
    public float GetMaxStamina() => stats.maxStamina;
}
