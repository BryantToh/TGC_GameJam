using UnityEngine;

public class StaminaUIFollowPlayer : MonoBehaviour
{
    [Header("References")]
    public RectTransform staminaUI; // The stamina UI (Slider or Image)
    public Transform player; // The player transform (for positioning the stamina bar)

    [Header("Positioning Options")]
    public Vector3 offset = new Vector3(0f, 1.5f, 0f); // The offset above the player for the stamina bar

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main; // Cache the main camera for world-to-screen conversion
    }

    private void Update()
    {
        // Convert the player's world position to screen space
        //Vector3 screenPosition = mainCamera.WorldToScreenPoint(player.position + offset);

        // Update the position of the stamina UI (on screen space)
        //staminaUI.position = screenPosition;

        Vector3 newPos = player.position + offset;
        staminaUI.position = newPos;
    }
}
