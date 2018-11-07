using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeType : MonoBehaviour {

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public bool endless;
}
