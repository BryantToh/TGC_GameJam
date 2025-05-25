using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatusUI : MonoBehaviour
{
    [Header("Health UI")]
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    [Header("Stamina UI")]
    public Slider staminaSlider;
    public TextMeshProUGUI staminaText;

    [Header("Target")]
    public PlayerStatus playerStatus;

    private void Start()
    {
        if (playerStatus != null)
        {
            playerStatus.onHealthChanged += UpdateHealthUI;
            playerStatus.onStaminaChanged += UpdateStaminaUI;

            // Initialize UI
            UpdateHealthUI(playerStatus.GetCurrentHealth(), playerStatus.GetMaxHealth());
            UpdateStaminaUI(playerStatus.GetCurrentStamina(), playerStatus.GetMaxStamina());
        }
    }

    private void UpdateHealthUI(float current, float max)
    {
        if (healthSlider != null)
            healthSlider.value = current;

        if (healthText != null)
            healthText.text = $"{Mathf.RoundToInt(current)} / {Mathf.RoundToInt(max)}";
    }

    private void UpdateStaminaUI(float current, float max)
    {
        if (staminaSlider != null)
            staminaSlider.value = current;

        if (staminaText != null)
            staminaText.text = $"{Mathf.RoundToInt(current)} / {Mathf.RoundToInt(max)}";
    }

    private void OnDestroy()
    {
        if (playerStatus != null)
        {
            playerStatus.onHealthChanged -= UpdateHealthUI;
            playerStatus.onStaminaChanged -= UpdateStaminaUI;
        }
    }
}
