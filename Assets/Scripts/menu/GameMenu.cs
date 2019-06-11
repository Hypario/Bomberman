using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameMenu : MonoBehaviour
{

    public GameObject pauseMenuUI; // handle the inputs from the pause menu

    public GameObject WinMenuUI; // needed to hide it when we want to continue the game after winning (demo)

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.instance.End) // if you hit escape and the game isn't over
        {
            Pause(); // pause or resume the game
        }
    }

    // function called when the replay button is hit
    public void replay()
    {
        if (GameManager.instance.paused())
        {
            GameManager.instance.pauseGame();
        }
        GameManager.instance.End = false; // the game is not ended anymore
        SceneManager.LoadScene(Random.Range(1, SceneManager.sceneCountInBuildSettings));
    }

    // function called when the menu button is hit
    public void LoadMenu()
    {
        if (GameManager.instance.paused()) // look if the game is paused
        {
            GameManager.instance.pauseGame(); // resume the game if paused
        }
        GameManager.instance.End = false; // the game is not ended anymore
        SceneManager.LoadScene(0); // load the menu
    }

    // function called when the quit button is hit
    public void QuitGame()
    {
        if (GameManager.instance.paused())
        {
            GameManager.instance.pauseGame();
        }
        Application.Quit();
    }

    // function called when the escape button is hit
    public void Pause()
    {
        if (!GameManager.instance.paused()) // if the game isn't paused
        {
            pauseMenuUI.SetActive(true); // show the pause menu UI
        } else
        {
            pauseMenuUI.SetActive(false); // else hide it
        }
        GameManager.instance.pauseGame(); // pause or resume the game
    }

    // button only for the demo
    public void Continue()
    {
        WinMenuUI.SetActive(false); // hide the Win menu
        GameManager.instance.pauseGame(); // unpause the game (can only be called by the WinMenu so when paused)
        GameManager.instance.End = false; // the game isn't ended anymore
    }
}
