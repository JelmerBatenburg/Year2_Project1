using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio : MonoBehaviour {

    public AudioMixer masterMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider SoundEffectSlider;

    public void Start()
    {
        bool master;
        float mastervalue;
        master = masterMixer.GetFloat("Master", out mastervalue);
        if (master)
        {
            masterSlider.value = mastervalue / 20;
        }
        bool music;
        float musicvalue;
        music = masterMixer.GetFloat("Music", out musicvalue);
        if (music)
        {
            musicSlider.value = musicvalue / 20;
        }
        bool effect;
        float effectvalue;
        effect = masterMixer.GetFloat("SoundEffects", out effectvalue);
        if (effect)
        {
            SoundEffectSlider.value = effectvalue / 20;
        }
    }

    public void OnMasterSliderChange()
    {
        masterMixer.SetFloat("Master", masterSlider.value * 20);
    }

    public void OnMusicSliderChange()
    {
        masterMixer.SetFloat("Music", musicSlider.value * 20);
    }

    public void OnSoundEffectSliderChange()
    {
        masterMixer.SetFloat("SoundEffects", SoundEffectSlider.value * 20);
    }
}
