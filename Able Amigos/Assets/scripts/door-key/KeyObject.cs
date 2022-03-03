using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour
{
    GameObject doorCheck;
    public GameObject keyObj;
    public AudioSource source;
    public AudioClip keyCollect;
    // Start is called before the first frame update
    void Start()
    {
        doorCheck = GameObject.FindGameObjectWithTag("Door");
        source = GameObject.FindWithTag("sound").GetComponent<AudioSource>();;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "player1" || collision.tag == "player2")
        {
            doorCheck.GetComponent<DoorCheck>().winCondition = true;
            //update sprite for the key
            keyObj.SetActive(true);
            source.clip = keyCollect;
            source.Play();
            Destroy(gameObject);
        }
    }
}
