using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatusUI : MonoBehaviour
{
    [Header("Health UI")]
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    [Header("Stamina UI")]
    public GameObject staminaUIObject;
    public Slider staminaSlider;
    public TextMeshProUGUI staminaText;

    [Header("Target")]
    public PlayerStatus playerStatus;

    [Header("Positioning")]
    public Transform playerTransform;
    public Vector3 offset = new Vector3(0, -1.5f, 0); // adjust as needed


    private void Start()
    {
        if (playerStatus != null)
        {
            playerStatus.onHealthChanged += UpdateHealthUI;
            playerStatus.onStaminaChanged += UpdateStaminaUI;

            // Initialize UI
            UpdateHealthUI(playerStatus.GetCurrentHealth(), playerStatus.GetMaxHealth());
            UpdateStaminaUI(playerStatus.GetCurrentStamina(), playerStatus.GetMaxStamina());

            if (staminaUIObject != null)
            {
                staminaUIObject.SetActive(false); // Start hidden
            }
        }
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(playerTransform.position + offset);
        }

        // Check if stamina is not full (regenerating) or actively decreasing (sprinting)
        float currentStamina = playerStatus.GetCurrentStamina();
        float maxStamina = playerStatus.GetMaxStamina();
        bool isRegenerating = currentStamina < maxStamina && !playerStatus.IsSprinting(); // You need this method
        bool isSprinting = playerStatus.IsSprinting(); // Implement this getter

        // Show only when sprinting or regenerating
        staminaUIObject.SetActive(isSprinting || isRegenerating);

        
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
