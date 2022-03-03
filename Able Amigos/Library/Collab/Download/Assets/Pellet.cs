using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pellet : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public GameObject spawner;
    public GameObject scoreManage;
    public AudioSource source;
    public AudioClip pelletWarp;

    // Start is called before the first frame update
    void Start()
    {
        scoreManage = ScoreManager.instance.gameObject;
        source = GameObject.FindWithTag("sound").GetComponent<AudioSource>();
        pelletWarp = Resources.Load<AudioClip>("Audio/pellet_warp");
    }


    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "player1" || other.tag == "player2")
        {
            //do nothing
        } else
        {
            if (spawner.tag == "player1")
            {
                scoreManage.GetComponent<ScoreManager>().ChangeScoreP1(1);
                source.clip = pelletWarp;
                source.Play();
            }
            else
            {
                scoreManage.GetComponent<ScoreManager>().ChangeScoreP2(1);
                source.clip = pelletWarp;
                source.Play();
            }
            Destroy(gameObject);
        }
    }
}
