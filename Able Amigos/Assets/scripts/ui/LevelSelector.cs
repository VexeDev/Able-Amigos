using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public SceneFader sceneFader;

    public void Level1()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Level2()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void loadLevel(int level)
    {
        sceneFader.FadeTo(level);
    }
}
