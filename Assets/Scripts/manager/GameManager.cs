using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public bool End = false;

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

    bool pause = false;

    public void LoseGame()
    {
        if (!pause)
        {
            GameObject.FindGameObjectWithTag("GameMenu").transform // find the GameMenu
                .Find("GameOverMenu").gameObject // find The GameOverMenu
                .SetActive(true); // show it
            pauseGame();
            End = true;
        }
    }

    public void WinGame()
    {
        if (!pause)
        {
            GameObject.FindGameObjectWithTag("GameMenu").transform // find the GameMenu
                .Find("WinMenu").gameObject // find the WinMenu
                .SetActive(true); // show it
            pauseGame();
            End = true;
        }
    }

    public void pauseGame()
    {
        if (!pause)
        {
            Time.timeScale = 0f;
            pause = true;
        } else
        {
            Time.timeScale = 1f;
            pause = false;
        }
    }

    public bool paused()
    {
        return pause;
    }
}
