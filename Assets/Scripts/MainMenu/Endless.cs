using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endless : MonoBehaviour {

    public GameObject levelInfo, gameType;
    public string levelName;

    public void OnButtonPress()
    {
        SceneManager.LoadScene("LoadScene");
        GameObject info = Instantiate(levelInfo, Vector3.zero, Quaternion.identity);
        info.GetComponent<LevelLoadInfo>().sceneName = levelName;
        GameObject type = Instantiate(gameType, Vector3.zero, Quaternion.identity);
        type.GetComponent<GameModeType>().endless = true;

        SceneManager.LoadScene("LoadScene");
    }
}
