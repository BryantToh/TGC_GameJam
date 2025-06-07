using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuController : UIShakeEffect
{
    [SerializeField] Animator m_transitionAnimator, m_settingsAnimator, m_menuAnimator;
    [SerializeField] GameObject m_animatedUI, m_settingsCanvas;
    [SerializeField] List<RectTransform> m_settingsRectTransform, m_menuRectTransform;
    int pauseCount = 0;
    bool countChanged = false;
    private void Start()
    {
        m_settingsCanvas.SetActive(false);

        if (m_animatedUI != null)
            m_animatedUI.SetActive(true);
    }
    private void Update()
    {
        if (!countChanged)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && pauseCount == 0)
            {
                OpenSettings();
                countChanged = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && pauseCount == 1)
            {
                Time.timeScale = 1.0f;
                CloseSettings();
                countChanged = true;
            }
        }
    }

    // ToDo: To update accordingly
    public void ChangeScene(string tag)
    {
        Time.timeScale = 1.0f;
        if (m_settingsCanvas.activeInHierarchy && SceneManager.GetActiveScene().name != "MainMenu")
        {
            CloseSettings();
        }
        StartCoroutine(ChangeScenes(tag));
    }
    public void OpenSettings()
    {
        if (m_menuRectTransform != null)
        {
            foreach (RectTransform item in m_menuRectTransform)
            {
                ShakeUI(item, 0.2f, 6f);
            }
        }

        StartCoroutine(OpenSettingsAftShake());

        if (m_animatedUI != null && m_menuAnimator != null)
            HideMenu();
    }
    public void CloseSettings()
    {
        if (m_menuRectTransform != null)
        {
            foreach (RectTransform item in m_menuRectTransform)
            {
                ShakeUI(item, 0.2f, 6f);
            }
        }
        StartCoroutine(CloseSettingsAftShake());
        if (m_animatedUI != null && m_menuAnimator != null)
            ShowMenu();
    }
    private void ShowMenu()
    {
        StartCoroutine(ShowMenuAftShake());
    }
    private void HideMenu()
    {
        StartCoroutine(HideMenuAftShake());
    }
    public void CloseApp()
    {
        Application.Quit();
    }
    private void ResetUI(bool showAnimatedUI, bool showSettingsCanvas)
    {
        m_animatedUI.SetActive(showAnimatedUI);
        m_settingsCanvas.SetActive(showSettingsCanvas);
    }

    IEnumerator ChangeScenes(string tag)
    {
        m_transitionAnimator.SetTrigger("Close");

        AnimatorStateInfo stateInfo = m_transitionAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1.0f)
        {
            yield return new WaitForSeconds(stateInfo.length);
            SceneManager.LoadScene(tag);
        }
    }

    IEnumerator OpenSettingsAftShake()
    {
        yield return new WaitForSeconds(0.15f);
        m_settingsCanvas.SetActive(true);
        m_settingsAnimator.SetTrigger("Show");

        AnimatorStateInfo stateInfo = m_settingsAnimator.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(stateInfo.length);

        if (m_animatedUI == null)
        {
            m_settingsCanvas.SetActive(true);
        }
        else
        {
            ResetUI(false, true);
        }

        if (SceneManager.GetActiveScene().name == "MainMenu")
            yield break;

        Time.timeScale = 0.0f;
        countChanged = false;
        pauseCount++;
    }

    IEnumerator CloseSettingsAftShake()
    {
        yield return new WaitForSeconds(0.15f);
        m_settingsAnimator.SetTrigger("Hide");

        AnimatorStateInfo stateInfo = m_settingsAnimator.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(stateInfo.length);

        if (m_animatedUI == null)
        {
            m_settingsCanvas.SetActive(false);
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
            yield break;
        countChanged = false;
        pauseCount--;
    }

    IEnumerator ShowMenuAftShake()
    {
        yield return new WaitForSeconds(0.15f);
        m_animatedUI.SetActive(true);
        m_menuAnimator.SetTrigger("Show");
        AnimatorStateInfo stateInfo = m_menuAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1.0f)
        {
            yield return new WaitForSeconds(stateInfo.length);
            ResetUI(true, false);
        }
    }

    IEnumerator HideMenuAftShake()
    {
        yield return new WaitForSeconds(0.15f);
        m_menuAnimator.SetTrigger("Hide");
        AnimatorStateInfo stateInfo = m_menuAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1.0f)
        {
            yield return new WaitForSeconds(stateInfo.length);
        }
    }
}
