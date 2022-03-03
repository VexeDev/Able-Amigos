using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
        pelletWarp = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/sounds/pellet_warp.ogg", typeof(AudioClip));
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
            // check for the force shrink
            if (spawner.tag == "player1")
            {
                spawner.GetComponent<player1Script>().ForceShrink();
            }
            else if (spawner.tag == "player2")
            {
                //spawner.GetComponent<player2Script>().ForceShrink();
            }
        } 
        else
        {
            if (spawner.tag == "player1")
            {
                collideType = 1;
                scoreManage.GetComponent<ScoreManager>().ChangeScoreP1(1);
                source.clip = pelletWarp;
                source.Play();
            }
            else
            {
                collideType = 2;
                scoreManage.GetComponent<ScoreManager>().ChangeScoreP2(1);
                source.clip = pelletWarp;
                source.Play();
            }
            Destroy(gameObject);
        }
    }
}
