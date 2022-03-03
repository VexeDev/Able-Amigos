using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite doorOpened;
    public Sprite doorClosed;
    public AudioSource source;
    public AudioClip doorOpen;
    bool audioPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.FindWithTag("sound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor ()
    {
        if (!audioPlayed)
        {
            source.clip = doorOpen;
            source.Play();
        }
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = doorOpened;
        source.clip = doorOpen;
    }

    public void CloseDoor ()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = doorClosed;
        //adjust sprites
    }
}
