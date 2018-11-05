using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

    public string loadScene;
    public Image fillbar;

    public IEnumerator Start()
    {
        LevelLoadInfo info = GameObject.FindWithTag("Manager").GetComponent<LevelLoadInfo>();
        loadScene = info.sceneName;
        Destroy(info.gameObject);

        AsyncOperation operation = SceneManager.LoadSceneAsync(loadScene);

        while (!operation.isDone)
        {
            fillbar.fillAmount = operation.progress;
            yield return null;
        }
    }
}
