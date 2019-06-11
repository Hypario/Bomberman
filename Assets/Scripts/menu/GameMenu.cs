using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    
    public static bool paused = false;

    public GameObject pauseMenuUI;

    public GameObject WinMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    // function called when the replay button is hit
    public void replay()
    {
        Resume();
        SceneManager.LoadScene(Random.Range(1, SceneManager.sceneCountInBuildSettings));
    }

    // function called when the resume or escape button is hit
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    // function called when the menu button is hit
    public void LoadMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

    // function called when the quit button is hit
    public void QuitGame()
    {
        Resume();
        Application.Quit();
    }

    // function called when the escape button is hit
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    // button only for the demo
    public void Continue()
    {
        WinMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }
}
