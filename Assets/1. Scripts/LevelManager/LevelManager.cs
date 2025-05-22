using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int levelIndex = 0;
    //[SerializeField] GameObject playerSpawnPoint;
    private GameObject currentLevel;
    [SerializeField] List<LevelSO> levelPrefabs = new List<LevelSO>();
    private void Start()
    {
        ActiveLevelPrefab(levelIndex);
    }
    public void LoadNextLevel()
    {
        if (levelIndex < levelPrefabs.Count && levelPrefabs[levelIndex].levelCompleted)
        {
            levelIndex++;
            if (levelIndex < levelPrefabs.Count)
                ActiveLevelPrefab(levelIndex);
            //Make player spawn
            //player.transform. = playerspawnpoint.transform
        }
        else
        {
            return;
        }
    }
    public void GetCurrLevelIndex()
    {
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
}
