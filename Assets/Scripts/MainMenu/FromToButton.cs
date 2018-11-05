using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromToButton : MonoBehaviour {

    public GameObject from, to;

    public void OnButtonPress()
    {
        from.SetActive(false);
        to.SetActive(true);
    }
}
