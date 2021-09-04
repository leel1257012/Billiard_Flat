using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audio;
    public Slider slider;
    private void Start()
    {
        audio = GameObject.Find("Audio").GetComponent<AudioSource>();
        slider.value = audio.volume;
    }
    public void Play()
    {
        //audio;
        SceneManager.LoadScene("MapSelect 1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
