using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject GameOverUI;

    public void LoseGame()
    {
        GameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
