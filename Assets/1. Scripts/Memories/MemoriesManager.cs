using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MemoriesManager : MonoBehaviour
{
    private float fadeDuration = 1.5f;
    [SerializeField] List<GameObject> memoryPrefabs = new List<GameObject>();
    private void Start()
    {
        for (int i = 0; i < memoryPrefabs.Count; i++)
        {
            foreach (Transform child in memoryPrefabs[i].transform)
            {
                child.gameObject.SetActive(false);
            }
            memoryPrefabs[i].SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)/*LevelManager.Instance.GetCurrLevel() == 2 && LevelManager.Instance.IsLevelCompleted()*/)
        {
            CanShowMemoryPanels();
        }
    }
    private void CanShowMemoryPanels()
    {
        for (int i = 0; i < memoryPrefabs.Count; i++)
        {
            memoryPrefabs[i].SetActive(true);
            foreach (Transform child in memoryPrefabs[i].transform)
            {
                child.gameObject.SetActive(true);
                Image image = child.gameObject.GetComponentInChildren<Image>();
                if (image != null)
                {
                    StartCoroutine(FadeIn(image));
                }
            }
        }
    }
    private IEnumerator FadeIn(Image image)
    {
        float elapsed = 0f;
        Color color = image.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / fadeDuration);
            image.color = color;
            yield return null;
        }

        color.a = 1f;
        image.color = color;
    }
}
