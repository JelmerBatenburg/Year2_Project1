using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    public string currenLevelName, mainMenuName;

    public void Restart()
    {
        SceneManager.LoadScene(currenLevelName);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
