using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public AudioSource source;
    public AudioClip pelletCollect1;
    public AudioClip pelletCollect2;

    void Start()
    {
        source = GameObject.FindWithTag("sound").GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetType() == typeof(BoxCollider2D))
            return;

        // for player 1
        if (other.gameObject.CompareTag("player1"))
        {
            source.clip = pelletCollect1;
            source.Play();
            Destroy(this.gameObject);
            ScoreManager.instance.ChangeScoreP1(coinValue);
        }
        else if (other.gameObject.CompareTag("player2"))
        {
            source.clip = pelletCollect2;
            source.Play();
            Destroy(this.gameObject);
            ScoreManager.instance.ChangeScoreP2(coinValue);
        }
    }
}