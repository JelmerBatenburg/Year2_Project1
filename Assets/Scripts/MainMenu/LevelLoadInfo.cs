using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadInfo : MonoBehaviour {

    public string sceneName;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
