using UnityEngine;

[CreateAssetMenu(fileName = "Input_", menuName = "ScriptableObjects/Input")]
public class InputSO : ScriptableObject 
{
    public KeyCode sprintKey;
    public KeyCode interactKey;

    public bool GetKey(KeyCode keyCode)
    {
        return Input.GetKey(keyCode);
    }

    public bool GetKeyDown(KeyCode keyCode)
    {
        return Input.GetKeyDown(keyCode);
    }
}
