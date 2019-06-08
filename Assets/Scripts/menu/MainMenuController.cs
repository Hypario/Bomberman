using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(Random.Range(1, SceneManager.sceneCountInBuildSettings));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
