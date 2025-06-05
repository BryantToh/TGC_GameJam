using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private int levelIndex = 0;
    private bool isNextLevelLoaded = false;
    private GameObject currentLevel;
    [SerializeField] List<LevelSO> levelPrefabs = new List<LevelSO>();
    [SerializeField] List<MemorySO> listOfMemory = new List<MemorySO>();
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < listOfMemory.Count; i++)
        {
            listOfMemory[i].Reset();
        }

        for (int i = 0; i < levelPrefabs.Count; i++)
        {
            levelPrefabs[i].levelCompleted = false;
        }
        ActiveLevelPrefab(levelIndex);

    }
    public void LoadNextLevel()
    {
        //if (!LevelTasksCompleted())
        //    return;
        if (levelIndex < levelPrefabs.Count && levelPrefabs[levelIndex].levelCompleted)
        {
            levelIndex++;
            if (levelIndex < levelPrefabs.Count)
                ActiveLevelPrefab(levelIndex);
            resetVariables();
            isNextLevelLoaded = true;
        }
        else
        {
            isNextLevelLoaded = false;
            return;
        }
    }

    public void SetCompleteLevel(bool isCompleted)
    {
        levelPrefabs[levelIndex].levelCompleted = isCompleted;
    }

    public bool IsNextLevelLoaded() => isNextLevelLoaded;
    public void CanLoadNextLevel(bool canLoad)
    {
        isNextLevelLoaded = canLoad;
    }
    public int GetCurrLevel()
    {
        return levelIndex;
    }
    private bool LevelTasksCompleted()
    {
        //if (levelPrefabs[levelIndex].Tasks == 3)
        //    return true;
        //else
        //    return false;
        return false;
    }
    public bool IsLevelCompleted()
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
        //levelPrefabs[levelIndex].Tasks = 0;
        
        LevelTasksCompleted();
    }
    public MemorySO GetMemory() => listOfMemory[levelIndex];
}
