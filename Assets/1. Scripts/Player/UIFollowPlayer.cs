using UnityEngine;

public class UIFollowPlayer : MonoBehaviour
{
    [Header("Stamina")]
    [SerializeField] private RectTransform staminaUI;

    //[Header("Health")]
    //[SerializeField] private RectTransform healthUI;

    public Transform player; 

    [Header("Offset")]
    public Vector3 staminaOffSet = new Vector3(0f, 1.5f, 0f); 
    //public Vector3 healthOffSet = new Vector3(0f, 1.5f, 0f); 


    private void Update()
    {

        Vector3 newStaminaPos = player.position + staminaOffSet;
        staminaUI.position = newStaminaPos;

        //Vector3 newHealthPos = player.position + healthOffSet;
        //healthUI.position = newHealthPos;
    }

}
