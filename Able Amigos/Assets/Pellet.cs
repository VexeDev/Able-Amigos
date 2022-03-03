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

    public int collideType = -1;

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
        if (other.tag == "player1" || other.tag == "player2")
        {
            // only detect the capsule collider
            if (other.GetType() == typeof(BoxCollider2D))
                return;

            // check for the force shrink
            if (spawner.tag == "player1")
            {
                spawner.GetComponent<player1Script>().ForceShrink();
            }
            else if (spawner.tag == "player2")
            {
                spawner.GetComponent<player2Script>().ForceShrink();
            }
        } 
        else
        {
            if (spawner.tag == "player1")
            {
                collideType = 1;
                source.clip = pelletWarp;
                source.Play();

                if (spawner.GetComponent<player1Script>().returnScore() == 0)
                    spawner.GetComponent<player1Script>().changeForceShrink();
                else
                    scoreManage.GetComponent<ScoreManager>().ChangeScoreP1(1);

            }
            else
            {
                collideType = 2;
                source.clip = pelletWarp;
                source.Play();

                if (spawner.GetComponent<player2Script>().returnScore() == 0)
                    spawner.GetComponent<player2Script>().changeForceShrink();
                else     
                    scoreManage.GetComponent<ScoreManager>().ChangeScoreP2(1);
            }
            Destroy(gameObject);
        }
    }
}
