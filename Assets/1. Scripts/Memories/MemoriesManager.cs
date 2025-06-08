using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoriesManager : MonoBehaviour
{
    public static MemoriesManager Instance;
    public System.Action OnMemorySequenceComplete;
    public bool allShown = false;

    private float fadeDuration = 1.5f;
    [SerializeField] private float delayBetweenMemories = 2f;
    [SerializeField] private List<GameObject> memoryPrefabs = new List<GameObject>();
    [SerializeField] private PlayerStatus status;

    private List<GameObject> spawnedMemories = new List<GameObject>();
    private bool memoryStarted = false; 

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (!memoryStarted &&
            LevelManager.Instance.GetCurrLevel() >= 2 &&
            (LevelManager.Instance.IsLevelCompleted() || status.GetCurrentHealth() <= 0f))
        {
            Debug.Log("Starting memory panel sequence");
            memoryStarted = true;
            CanShowMemoryPanels();
        }
    }

    private void CanShowMemoryPanels()
    {
        StartCoroutine(DisplayMemories());
    }

    public void StartMemorySequence()
    {
        if (!memoryStarted)
        {
            memoryStarted = true;
            StartCoroutine(DisplayMemories());
        }
    }

    private IEnumerator DisplayMemories()
    {
        yield return new WaitForSeconds(1f);

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            player.SetActive(false);

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

        OnMemorySequenceComplete?.Invoke();
        allShown = true;
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
