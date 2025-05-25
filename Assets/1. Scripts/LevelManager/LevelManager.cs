using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int levelIndex = 0;
    private GameObject currentLevel;
    public static LevelManager Instance;
    [SerializeField] List<LevelSO> levelPrefabs = new List<LevelSO>();
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ActiveLevelPrefab(levelIndex);
    }
    public void LoadNextLevel()
    {
        if (!LevelTasksCompleted())
            return;

        if (levelIndex < levelPrefabs.Count && levelPrefabs[levelIndex].levelCompleted)
        {
            levelIndex++;
            if (levelIndex < levelPrefabs.Count)
                ActiveLevelPrefab(levelIndex);
            resetVariables();
        }
        else
        {
            return;
        }
    }
    public int GetCurrLevel()
    {
        return levelIndex;
    }
    private bool LevelTasksCompleted()
    {
        if (levelPrefabs[levelIndex].Tasks == 3)
            return true;
        else
            return false;
    }
    public bool LevelCompletion()
    {
        return levelPrefabs[levelIndex].levelCompleted;
    }
    private void RestartLevel()
    {
        if (levelIndex > levelPrefabs.Count)
            return;

        ActiveLevelPrefab(levelIndex);
    }
    private void ActiveLevelPrefab(int index)
    {
        if (currentLevel != null)
            Destroy(currentLevel);

        currentLevel = Instantiate(levelPrefabs[index].levelPrefab);
    }

    private void resetVariables()
    {
        levelPrefabs[levelIndex].Tasks = 0;
        LevelTasksCompleted();
    }
}
