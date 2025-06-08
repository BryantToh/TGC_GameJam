using UnityEngine;
using UnityEngine.UI;

public class EndingImages : MonoBehaviour
{
    [SerializeField] private GameObject badEndImage;
    [SerializeField] private GameObject goodEndImage;
    [SerializeField] private GameObject fade;
    bool imageSpawned = false;
    private void Start()
    {
        badEndImage.SetActive(false);
        goodEndImage.SetActive(false);
    }
    private void Update()
    {
        MemoriesManager.Instance.OnMemorySequenceComplete += MemorySequenceFinished;
    }
    private void MemorySequenceFinished()
    {
        if (MemoriesManager.Instance.allShown && !imageSpawned && LevelManager.Instance.IsLevelCompleted() && LevelManager.Instance.GetCurrLevel() == 2)
        {
            imageSpawned = true;
            fade.SetActive(false);
            goodEndImage.SetActive(true);
        }
        else if (!LevelManager.Instance.IsLevelCompleted() && LevelManager.Instance.GetCurrLevel() == 2 && !imageSpawned && MemoriesManager.Instance.allShown)
        {
            imageSpawned = true;
            fade.SetActive(false);
            badEndImage.SetActive(true);
        }
    }
}
