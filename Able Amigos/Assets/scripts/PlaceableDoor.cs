using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableDoor : MonoBehaviour
{
    bool isOpen = false;
    public Sprite openDoor;
    public Sprite closeDoor;
    public AudioSource source;
    public AudioClip doorOpen;

    void Start()
    {
        source = GameObject.FindWithTag("sound").GetComponent<AudioSource>();
    }

    public void Toggle ()
    {
        if(isOpen)
        {
            this.GetComponent<BoxCollider2D>().enabled = true;
            this.GetComponent<SpriteRenderer>().sprite = closeDoor;
            isOpen = false;
        } else
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<SpriteRenderer>().sprite = openDoor;
            source.clip = doorOpen;
            source.Play();
            isOpen = true;
        }
    }
}
