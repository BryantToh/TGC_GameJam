using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] string levelName;
    public GameObject levelPrefab;
    public int task1 = 0, task2 = 0, task3 = 0;
    public bool TasksCompleted = false;
    public bool levelCompleted = false;
}
