using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] string levelName;
    public GameObject levelPrefab;
    public bool levelCompleted = false;
    //public int levelIndex;
}
