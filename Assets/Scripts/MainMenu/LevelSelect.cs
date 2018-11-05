using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    public string sceneName;
    public int levelNumber ,waves, startingAmount;
    [Range(1, 5)]
    public int difficulty;
    public Sprite levelImage;

    public void OnButtonPress()
    {
        GameObject.FindWithTag("Manager").GetComponent<StartLevel>().SetInfo(sceneName, levelNumber, waves, levelImage,difficulty,startingAmount);
    }
}
