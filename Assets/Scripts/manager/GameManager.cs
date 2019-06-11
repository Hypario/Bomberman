using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // the win and the game over menu
    public GameObject GameOverUI;
    public GameObject WinUI;

    public void LoseGame()
    {
        GameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void WinGame()
    {
        WinUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
