using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public int dubble, tripple;
    public Slider slider;

    public void OnSliderChange()
    {
        Time.timeScale = slider.value;
    }

    public void Normal()
    {
        Time.timeScale = 1;
        slider.value = 1;
    }

    public void Dubble()
    {
        Time.timeScale = dubble;
        slider.value = dubble;
    }

    public void Tripple()
    {
        Time.timeScale = tripple;
        slider.value = tripple;
    }
}
