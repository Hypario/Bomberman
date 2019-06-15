using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    // called when we hit the play button
    public void PlayGame()
    {
        SceneManager.LoadScene(Random.Range(1, SceneManager.sceneCountInBuildSettings));
    }

    // called when we hit the quit button
    public void QuitGame()
    {
        Application.Quit();
    }

}
