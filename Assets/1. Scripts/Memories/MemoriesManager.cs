using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MemoriesManager : MonoBehaviour
{
    private float fadeDuration = 1.5f;
    [SerializeField] private float delayBetweenMemories = 2f;
    [SerializeField] private List<GameObject> memoryPrefabs = new List<GameObject>();

    private List<GameObject> spawnedMemories = new List<GameObject>();

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)/*LevelManager.Instance.GetCurrLevel() == 3 && LevelManager.Instance.IsLevelCompleted()*/)
        {
            CanShowMemoryPanels();
        }
    }

    private void CanShowMemoryPanels()
    {
        StartCoroutine(DisplayMemories());
    }

    private IEnumerator DisplayMemories()
    {
        foreach (GameObject prefab in memoryPrefabs)
        {
            GameObject memoryInstance = Instantiate(prefab, transform);
            memoryInstance.name = "Memory_" + spawnedMemories.Count;
            spawnedMemories.Add(memoryInstance);

            List<Image> imagesToFade = new List<Image>();

            foreach (Transform child in memoryInstance.transform)
            {
                child.gameObject.SetActive(true);
                Image[] images = child.GetComponentsInChildren<Image>(true);

                foreach (Image img in images)
                {
                    img.gameObject.SetActive(true);

                    Color tempColor = img.color;
                    tempColor.a = 0f;
                    img.color = tempColor;

                    imagesToFade.Add(img);
                }
            }

            memoryInstance.SetActive(true);

            yield return StartCoroutine(FadeImagesOrder(imagesToFade));

            yield return new WaitForSeconds(delayBetweenMemories);

            yield return StartCoroutine(FadeImagesOut(imagesToFade));

            Destroy(memoryInstance);
        }
    }

    private IEnumerator FadeImagesOrder(List<Image> images)
    {
        foreach (Image img in images)
        {
            img.gameObject.SetActive(true);
            yield return StartCoroutine(FadeIn(img));
        }
    }

    private IEnumerator FadeImagesOut(List<Image> images)
    {
        foreach (Image img in images)
        {
            yield return StartCoroutine(FadeOut(img));
        }
    }

    private IEnumerator FadeIn(Image image)
    {
        float elapsed = 0f;
        Color color = image.color;
        color.a = 0f;
        image.color = color;

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

    private IEnumerator FadeOut(Image image)
    {
        float elapsed = 0f;
        Color color = image.color;
        color.a = 1f;
        image.color = color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsed / fadeDuration));
            image.color = color;
            yield return null;
        }

        color.a = 0f;
        image.color = color;
    }
}
