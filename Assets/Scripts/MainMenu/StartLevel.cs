using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour {

    public Text nameInput, waveInput, amountInput;
    public Image spriteInput;
    public string currentSelectedLevel;
    public GameObject loadScene, gameType;

    public void SetInfo(string level, int name, int waves, Sprite image, int difficulity,int startingAmount)
    {
        currentSelectedLevel = level;
        spriteInput.sprite = image;
        nameInput.text = "Level " + name.ToString();
        waveInput.text = waves.ToString();
        amountInput.text = startingAmount.ToString();
    }

    public void StartButton()
    {
        if(currentSelectedLevel != "")
        {
            GameObject info = Instantiate(loadScene, Vector3.zero, Quaternion.identity);
            info.GetComponent<LevelLoadInfo>().sceneName = currentSelectedLevel;
            GameObject type = Instantiate(gameType, Vector3.zero, Quaternion.identity);
            type.GetComponent<GameModeType>().endless = false;

            SceneManager.LoadScene("LoadScene");
        }
    }
}
