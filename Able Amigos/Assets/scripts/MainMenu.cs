using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public GameObject originalTitle;
    public GameObject beginningText;
    public GameObject menuOptions;
    public SceneFader sceneFader;
    public GameObject optionsMenu;
    bool optionsMenuB = false;
    public AudioMixer audioMix;

    /*public GameObject[] menuButtons = new GameObject[3];
     int currentButton = 0;*/

    public void Start()
    {
        audioMix.SetFloat("volume", PlayerPrefs.GetFloat("volume"));
    }

    public void PlayGame()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OptionsMenu()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            originalTitle.SetActive(false);
            beginningText.SetActive(false);
            menuOptions.SetActive(true);
        }

        /*if(Input.GetKeyDown("W"))
        {
            currentButton += 1;
            if (currentButton > menuButtons.Length)
            {
                currentButton = 0;
            }
        }
        else if (Input.GetKeyDown("S"))
        {
            currentButton -= 1;
            if (currentButton < menuButtons.Length)
            {
                currentButton = menuButtons.Length;
            }
        }*/

    }
}
