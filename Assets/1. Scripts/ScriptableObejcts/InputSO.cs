using UnityEngine;

[CreateAssetMenu(fileName = "Input_", menuName = "ScriptableObjects/Input")]
public class InputSO : ScriptableObject 
{
    public KeyCode sprintKey;
    public KeyCode interactKey;

    public bool IsKeyPressed(KeyCode keyCode)
    {
        return Input.GetKey(keyCode);
    }
}
