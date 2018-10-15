using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public int dubble, tripple;

    public void Pauze()
    {
        Time.timeScale = 0;
    }

    public void Normal()
    {
        Time.timeScale = 1;
    }

    public void Dubble()
    {
        Time.timeScale = dubble;
    }

    public void Tripple()
    {
        Time.timeScale = tripple;
    }
}
