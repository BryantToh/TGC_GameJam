using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] GameObject completedImage1, completedImage2, completedImage3;
    [SerializeField] Slider levelSlider1, levelSlider2;
    [SerializeField] float sliderDuration;
    [SerializeField] GameObject levelTransitionObj, transitionBG;
    private void Start()
    {
        HideAll();
    }

    void Update()
    {
        if (LevelManager.Instance.IsLevelCompleted() && !LevelManager.Instance.IsLastLevel())
        {
            AudioManager.instance.PlaySFX("nextlevel", 0.5f);
            levelTransitionObj.SetActive(true);
            transitionBG.SetActive(true);
            LevelTransitionSlider();
        }
    }

    private void LevelTransitionSlider()
    {
        if (LevelManager.Instance.GetCurrLevel() == 0)
        {
            StartCoroutine(IncreaseSlider(levelSlider1));
        }
        else if (LevelManager.Instance.GetCurrLevel() == 1)
        {
            StartCoroutine(IncreaseSlider(levelSlider2));
        }
    }

    private void HideAll()
    {
        transitionBG.SetActive(false);
        levelTransitionObj.SetActive(false);
    }

    private IEnumerator IncreaseSlider(Slider slider)
    {
        float elapsed = 0f;

        while (elapsed < sliderDuration)
        {
            elapsed += Time.deltaTime;
            slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, elapsed / sliderDuration);
            yield return null;
        }
        LevelManager.Instance.LoadNextLevel();
        HideAll();
        levelSlider1.value = 1f;
    }
}
