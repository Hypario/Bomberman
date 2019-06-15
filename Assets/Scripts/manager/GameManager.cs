using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // instance of the object
    public static GameManager instance;

    // true if the game is over
    public bool End = false;

    // true if the game is paused
    bool pause = false;

    // singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
            return;
        }
    }

    // called when the player get hit by an explosion
    public void LoseGame()
    {
        if (!pause)
        {
            GameObject.FindGameObjectWithTag("GameMenu").transform // find the GameMenu
                .Find("GameOverMenu").gameObject // find The GameOverMenu
                .SetActive(true); // show it
            PauseGame(); // pause the game
            End = true; // the game is ended
        }
    }

    // called when the AI get hit by an explosion
    public void WinGame()
    {
        if (!pause)
        {
            GameObject.FindGameObjectWithTag("GameMenu").transform // find the GameMenu
                .Find("WinMenu").gameObject // find the WinMenu
                .SetActive(true); // show it
            PauseGame(); // pause the game
            End = true; // the game is ended
        }
    }

    // pause or resume the game
    public void PauseGame()
    {
        // if the game isn't paused
        if (!pause)
        {
            Time.timeScale = 0f; // the time scale is set to 0
            pause = true; // the game is stated as pause
        } else
        {
            Time.timeScale = 1f; // the time scale is set to 1
            pause = false; // the game is resumed
        }
    }

    // return true if the game is paused, else false
    public bool IsPaused()
    {
        return pause;
    }
}
