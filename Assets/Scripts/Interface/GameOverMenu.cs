using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    public string currenLevelName, mainMenuName;
    public Base enemyBase;
    public GameObject gameModeType;

    public void Restart()
    {
        GameObject g = Instantiate(gameModeType, Vector3.zero, Quaternion.identity);
        g.GetComponent<GameModeType>().endless = enemyBase.endless;
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
