using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] string levelName;
    public GameObject levelPrefab;
    public int Tasks = 0;
    public bool levelCompleted = false;
}
