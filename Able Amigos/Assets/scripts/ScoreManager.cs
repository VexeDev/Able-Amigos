using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI textP1;
    public TextMeshProUGUI textP2;
    int scoreP1 = 0;
    int scoreP2 = 0;

    public Image p1Score;
    public Image p2Score;

    GameObject p1;
    GameObject p2;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        p1 = GameObject.Find("player1");
        p2 = GameObject.Find("player2");
        UpdateBar(1);
        UpdateBar(2);
    }

    public void ChangeScoreP1(int coinValue)
    {
        scoreP1 += coinValue;
        textP1.text = scoreP1.ToString();
        player1Script.inst.changeScore(scoreP1);
        UpdateBar(1);
    }

    public void ChangeScoreP2(int coinValue)
    {
        scoreP2 += coinValue;
        textP2.text = scoreP2.ToString();
        player2Script.inst.changeScore(scoreP2);
        UpdateBar(2);
    }

    public void subScoreP1(int s)
    {
        scoreP1 -= s;
        textP1.text = scoreP1.ToString();
        UpdateBar(1);
    }

    public void subScoreP2(int s)
    {
        scoreP2 -= s;
        textP2.text = scoreP2.ToString();
        UpdateBar(2);
    }

    public void forceChange1(int s)
    {
        scoreP1 = s;
        textP1.text = scoreP1.ToString();
        UpdateBar(1);
    }

    public void forceChange2(int s)
    {
        scoreP2 = s;
        textP1.text = scoreP2.ToString();
        UpdateBar(2);
    }

    void UpdateBar(int player)
    {
        if(player == 1)
        {
            if(scoreP1 == 0)
            {
                p1Score.sprite = p1.GetComponent<player1Script>().empty;
            } else if (scoreP1 == 1)
            {
                p1Score.sprite = p1.GetComponent<player1Script>().oneBar;
            } else if (scoreP1 == 2)
            {
                p1Score.sprite = p1.GetComponent<player1Script>().twoBar;
            } else
            {
                p1Score.sprite = p1.GetComponent<player1Script>().threeBar;
            }
        } else
        {
            if (scoreP2 == 0)
            {
                p2Score.sprite = p2.GetComponent<player2Script>().empty;
            }
            else if (scoreP2 == 1)
            {
                p2Score.sprite = p2.GetComponent<player2Script>().oneBar;
            }
            else if (scoreP2 == 2)
            {
                p2Score.sprite = p2.GetComponent<player2Script>().twoBar;
            }
            else
            {
                p2Score.sprite = p2.GetComponent<player2Script>().threeBar;
            }
        }
    }
}
