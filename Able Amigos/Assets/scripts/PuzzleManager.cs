using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    List<string> buttonOrder =  new List<string>();
    public List<string> playerButtonPress =  new List<string>();
    bool puzzleComplete  = false;
    public GameObject barrier;
    public GameObject progressIndicator;
    public Material incorrect;
    public Material correct;
    public Material blank;
    float timer = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            
            buttonOrder.Add("red");
            buttonOrder.Add("yellow");
            buttonOrder.Add("blue");
            buttonOrder.Add("green");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (buttonOrder.Count == playerButtonPress.Count)
        {
            for (int i = 0; i < buttonOrder.Count; i++)
            {
                if(buttonOrder[i] == playerButtonPress[i])
                {
                    puzzleComplete = true;
                }
                else
                {
                    puzzleComplete = false;
                    progressIndicator.GetComponent<MeshRenderer>().material = incorrect;
                    ResetPuzzle();
                    break;

                }
            }
        }
    

        if (puzzleComplete)
        {
            progressIndicator.GetComponent<MeshRenderer>().material = correct;
            barrier.SetActive(false);
        }


    }

    void ResetPuzzle()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            playerButtonPress.Clear();
            progressIndicator.GetComponent<MeshRenderer>().material = blank;
            timer = 0.5f;
        }
    }
}
