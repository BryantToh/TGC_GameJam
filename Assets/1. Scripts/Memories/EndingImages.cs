using UnityEngine;
using UnityEngine.UI;

public class EndingImages : MonoBehaviour
{
    [SerializeField] private GameObject badEndImage;
    [SerializeField] private GameObject goodEndImage;
    [SerializeField] private GameObject fade;
    [SerializeField] private PlayerStatus status;
    bool imageSpawned = false;
    private void Start()
    {
        badEndImage.SetActive(false);
        goodEndImage.SetActive(false);
    }
    private void Update()
    {
        MemoriesManager.Instance.OnMemorySequenceComplete += MemorySequenceFinished;

        if (!LevelManager.Instance.IsLevelCompleted() && LevelManager.Instance.GetCurrLevel() == 2 && !imageSpawned && MemoriesManager.Instance.allShown/*status.gameObject.transform.position.y < -42f*/)
        {
            imageSpawned = true;
            fade.SetActive(false);
            badEndImage.SetActive(true);
        }
    }
    private void MemorySequenceFinished()
    {
        if (/*MemoriesManager.Instance.allShown && */!imageSpawned && LevelManager.Instance.IsLevelCompleted() && LevelManager.Instance.GetCurrLevel() == 2)
        {
            imageSpawned = true;
            fade.SetActive(false);
            goodEndImage.SetActive(true);
        }
        
    }
}
