using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Invoke(nameof(LoadNextScene), 0.75f);
    }

    public void QuitGame()
    {
        Invoke(nameof(LoadQuitGame), 0.75f);
    }

    public void GoToMainMenu()
    {
        Invoke(nameof(LoadMainMenu), 0.75f);
    }
    public void GoToSettingsMenu()
    {
        Invoke(nameof(LoadSettingsMenu), 0.75f);
    }
    public void GoToTutorial()
    {
        Invoke(nameof(LoadTutorial), 0.75f);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadQuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void LoadSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
