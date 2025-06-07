using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingHandler : MonoBehaviour
{
    [SerializeField] GameObject settingsObj;
    [SerializeField] Animator settingsAnim;
    int pauseCount = 0;
    bool countChanged = false;
    void Start()
    {
        settingsObj.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseCount == 0)
        {
            pauseCount++;
            ShowSettings();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseCount == 1)
        {
            Time.timeScale = 1.0f;
            pauseCount--;
            HideSettings();
        }
    }

    private void ShowSettings()
    {
        StartCoroutine(OpenSettings());
    }
    public void HideSettings()
    {
        StartCoroutine(CloseSettings());
    }

    IEnumerator OpenSettings()
    {
        yield return new WaitForSeconds(0.15f);
        settingsObj.SetActive(true);
        settingsAnim.SetTrigger("Show");

        AnimatorStateInfo stateInfo = settingsAnim.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(stateInfo.length);
        Time.timeScale = 0.0f;

        //if (m_animatedUI == null)
        //{
        //    m_settingsCanvas.SetActive(true);
        //}
        //else
        //{
        //    ResetUI(false, true);
        //}

        //if (SceneManager.GetActiveScene().name == "MainMenu")
        //    yield break;
    }

    IEnumerator CloseSettings()
    {
        yield return new WaitForSeconds(0.15f);
        settingsAnim.SetTrigger("Hide");

        AnimatorStateInfo stateInfo = settingsAnim.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(stateInfo.length);

        settingsObj.SetActive(false);
    }
}
