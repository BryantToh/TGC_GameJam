using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    int pause = 0;
    [SerializeField] GameObject settingsObj;
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void GoToGame()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenSettings()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Pausing();
        }
        settingsObj.SetActive(true);
    }
    public void Pausing()
    {
        if (pause == 0)
        {
            pause = 1;
            Time.timeScale = 1;
        }
        else if (pause == 1)
        {
            pause = 0;
            Time.timeScale = 0;
        }
    }
    public void CloseSettings()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Pausing();
        }
        settingsObj.SetActive(false);
    }
}
