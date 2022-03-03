using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public SceneFader sceneFader;
    public AudioMixer audioMixer;
    public GameObject slider;

    public void Start()
    {
        slider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("volume");
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("volume"));
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            sceneFader.FadeTo(0);
        }
    }
}
